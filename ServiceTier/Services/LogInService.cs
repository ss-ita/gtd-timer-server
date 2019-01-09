using System;
using System.Security.Claims;
using System.Text;

using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;
using Common.ModelsDTO;
using Common.Constant;
using ServiceTier.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Common.Exceptions;
using Newtonsoft.Json;
using Common.IoC;
using System.Net.Http;
using Timer.DAL.Extensions;

namespace gtdtimer.Services
{
    public class LogInService : ILogInService
    {
        private IUnitOfWork userManager;
        private static readonly HttpClient Client = new HttpClient();

        public LogInService(IUnitOfWork userManager)
        {
            this.userManager = userManager;
        }

        public string CreateToken(LoginDTO model)
        {
            var user = userManager.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!userManager.UserManager.CheckPasswordAsync(user, model.Password).Result)
            {
                throw new LoginFailedException();
            }

            var token = JWTManager.GenerateToken(user);

            return token;
        }

        public string CreateTokenWithGoogle(SocialAuthDTO accessToken)
        {
            string userInfoResponse = Client.GetStringAsync($"{Constants.GoogleResponsePath}{accessToken.AccessToken}").Result;

            var userInfo = JsonConvert.DeserializeObject<GoogleAuthUserData>(userInfoResponse);
            User user = userManager.UserManager.FindByEmailAsync(userInfo.Email).Result;

            if (user == null)
            {
                user = userInfo.ToUser();
                var result = userManager.UserManager.CreateAsync(user, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8)).Result;

                if (!result.Succeeded)
                {
                    throw new UserNotAddedException();
                }
            }

            string jwt = JWTManager.GenerateToken(user);

            return jwt;
        }

        public string CreateTokenWithFacebook(SocialAuthDTO accessToken)
        {
            string userInfoResponse = Client.GetStringAsync($"{Constants.FacebookResponsePath}{accessToken.AccessToken}").Result;

            var userInfo = JsonConvert.DeserializeObject<FacebookAuthUserData>(userInfoResponse);
            User user = userManager.UserManager.FindByEmailAsync(userInfo.Email).Result;

            if (user == null)
            {
                user = userInfo.ToUser();
                var result = userManager.UserManager.CreateAsync(user, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8)).Result;

                if (!result.Succeeded)
                {
                    throw new UserNotAddedException();
                }
            }

            string jwt = JWTManager.GenerateToken(user);

            return jwt;
        }
    }
}
