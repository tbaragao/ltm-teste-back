using IdentityServer4.Test;
using Sso.Server.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sso.Server.Api
{
    public class UserServices
    {

        public async Task<User> Auth(string userName, string password)
        {
            return await Task.Run(() =>
            {
                var user = Config.GetUsers()
                    .Where(_ => _.Username.ToLower() == userName.ToLower())
                    .Where(_ => _.Password.ToLower() == password.ToLower())
                    .SingleOrDefault();

                return user;
            });
        }

        public async Task<User> AuthByExternalLogin(IEnumerable<Claim> claims, TestUser user)
        {
            return await Task.Run(() =>
            {
                var email = claims.Where(_ => _.Type.ToLower() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").SingleOrDefault().Value;
                var nome = claims.Where(_ => _.Type.ToLower() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").SingleOrDefault().Value;

                return new User
                {
                    SubjectId = "1",
                    Username = nome,
                    Claims = Config.Claims(nome, "Google's user", email)
                };
            });

        }

    }
}
