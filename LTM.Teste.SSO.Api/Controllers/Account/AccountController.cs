using IdentityModel;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using Sso.Server.Api;
using Sso.Server.Api.Model;

namespace IdentityServer4.Quickstart.UI
{
    [SecurityHeaders]
    public class AccountController : Controller
    {
        private readonly UserServices _usersServices;
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly AccountService _account;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            IEventService events,
            TestUserStore users = null)
        {
            _users = users ?? new TestUserStore(TestUsers.Users);
            _usersServices = new UserServices();
            _interaction = interaction;
            _events = events;
            _account = new AccountService(interaction, httpContextAccessor, clientStore);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await _account.BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
                return await ExternalLogin(vm.ExternalLoginScheme, returnUrl);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersServices.Auth(model.Username, model.Password);
                if (user.IsNotNull())
                {
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    };

                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username));
                    await HttpContext.Authentication.SignInAsync(user.SubjectId, user.Username, user.Claims.ToArray());

                    if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);

                    return Redirect("~/");
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));

                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }

            var vm = await _account.BuildLoginViewModelAsync(model);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            returnUrl = Url.Action("ExternalLoginCallback", new { returnUrl = returnUrl });

            if (AccountOptions.WindowsAuthenticationSchemes.Contains(provider))
            {
                if (HttpContext.User is WindowsPrincipal wp)
                {
                    var props = new AuthenticationProperties();
                    props.Items.Add("scheme", AccountOptions.WindowsAuthenticationProviderName);

                    var id = new ClaimsIdentity(provider);
                    id.AddClaim(new Claim(JwtClaimTypes.Subject, HttpContext.User.Identity.Name));
                    id.AddClaim(new Claim(JwtClaimTypes.Name, HttpContext.User.Identity.Name));

                    if (AccountOptions.IncludeWindowsGroups)
                    {
                        var wi = wp.Identity as WindowsIdentity;
                        var groups = wi.Groups.Translate(typeof(NTAccount));
                        var roles = groups.Select(x => new Claim(JwtClaimTypes.Role, x.Value));
                        id.AddClaims(roles);
                    }

                    await HttpContext.Authentication.SignInAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme, new ClaimsPrincipal(id), props);
                    return Redirect(returnUrl);
                }
                else
                    return new ChallengeResult(AccountOptions.WindowsAuthenticationSchemes);
            }
            else
            {
                var props = new AuthenticationProperties
                {
                    RedirectUri = returnUrl,
                    Items = { { "scheme", provider } }
                };
                return new ChallengeResult(provider, props);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
        {
            var info = await HttpContext.Authentication.GetAuthenticateInfoAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            var tempUser = info?.Principal;
            if (tempUser == null)
                throw new Exception("External authentication error");

            var claims = tempUser.Claims.ToList();

            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new Exception("Unknown userid");

            claims.Remove(userIdClaim);
            var provider = info.Properties.Items["scheme"];
            var userId = userIdClaim.Value;

            var userTest = _users.FindByExternalProvider(provider, userId);
            if (userTest == null)
                userTest = _users.AutoProvisionUser(provider, userId, claims);

            var additionalClaims = new List<Claim>();

            var user = await this._usersServices.AuthByExternalLogin(claims, userTest);
            if (user.Claims.IsAny())
            {
                foreach (var item in user.Claims)
                    additionalClaims.Add(new Claim(item.Type, item.Value));
            }

            var sid = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
                additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));

            AuthenticationProperties props = null;
            var id_token = info.Properties.GetTokenValue("id_token");
            if (id_token != null)
            {
                props = new AuthenticationProperties();
                props.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = id_token } });
            }

            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, userId, user.SubjectId, user.Username));
            await HttpContext.Authentication.SignInAsync(user.SubjectId, user.Username, provider, props, additionalClaims.ToArray());
            await HttpContext.Authentication.SignOutAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);

            if (_interaction.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return Redirect("~/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId, string returnUrl)
        {
            var vm = await _account.BuildLoggedOutViewModelAsync(logoutId);
            if (vm.TriggerExternalSignout)
            {
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
                try
                {
                    await HttpContext.Authentication.SignOutAsync(vm.ExternalAuthenticationScheme, new AuthenticationProperties { RedirectUri = url });
                }
                catch (NotSupportedException)
                { }
                catch (InvalidOperationException) 
                { }
            }

            await HttpContext.Authentication.SignOutAsync();

            var user = await HttpContext.GetIdentityServerUserAsync();
            if (user != null)
                await _events.RaiseAsync(new UserLogoutSuccessEvent(user.GetSubjectId(), user.GetName()));

            if (returnUrl != null)
                return Redirect(returnUrl);

            return View("LoggedOut", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            var vm = await _account.BuildLoggedOutViewModelAsync(model.LogoutId);
            if (vm.TriggerExternalSignout)
            {
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
                try
                {
                    await HttpContext.Authentication.SignOutAsync(vm.ExternalAuthenticationScheme, new AuthenticationProperties { RedirectUri = url });
                }
                catch (NotSupportedException)
                { }
                catch (InvalidOperationException)
                { }
            }

            await HttpContext.Authentication.SignOutAsync();

            var user = await HttpContext.GetIdentityServerUserAsync();
            if (user != null)
                await _events.RaiseAsync(new UserLogoutSuccessEvent(user.GetSubjectId(), user.GetName()));

            if (model.ReturnUrl != null)
                return Redirect(model.ReturnUrl);

            return View("LoggedOut", vm);
        }
    }
}
