using Common.Domain.Base;
using IdentityModel;
using IdentityServer4.Models;
using Newtonsoft.Json;
using Sso.Server.Api.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

namespace Sso.Server.Api
{
    public class Config
    {
        public static User MakeUsersAdmin()
        {
            return new User
            {
                SubjectId = "1",
                Username = "Administrador",
                Password = "123456",
                Claims = ClaimsForAdmin("Administrador", "admin@email.com.br")
            };
        }

        public static List<Claim> ClaimsForAdmin(string name, string email)
        {
            var tools = new List<dynamic>
            {
                new { name = "Teste", value = "/teste" },
            };

            var _toolsForAdmin = JsonConvert.SerializeObject(tools);

            return new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, name),
                new Claim(JwtClaimTypes.Email, email),
                new Claim("tools", _toolsForAdmin),
                new Claim("role", "Administrador"),
            };
        }
        
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("ssoltmteste", "SPA Client Implicit")
                {
                    Scopes = new List<Scope>()
                    {
                        new Scope
                        {
                            UserClaims = new List<string> {"name", "openid", "email", "role", "tools"},
                            Name = "ltmteste",
                            Description = "SPA Client Implicit",
                        }
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<Client> GetClients(ConfigSettingsBase settings)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ltmteste-spa",
                    ClientSecrets = { new Secret("ltmteste-spa".Sha256()) },

                    AccessTokenLifetime = 60,
                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        settings.RedirectUris
                    },
                    PostLogoutRedirectUris =
                    {
                        settings.PostLogoutRedirectUris
                    },

                    AllowedCorsOrigins = { settings.ClientAuthorityEndPoint },

                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Profile,
                        StandardScopes.Email,
                        "ltmteste"
                    }
                }

            };
        }

        public static List<User> GetUsers()
        {
            return new List<User>()
            {
                MakeUsersAdmin()
            };
        }

    }
}
