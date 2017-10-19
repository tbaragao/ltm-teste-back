using Sso.Server.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
