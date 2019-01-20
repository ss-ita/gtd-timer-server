using System;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;
using Common.ModelsDTO;
using Common.Constant;
using ServiceTier.Services;
using Common.Exceptions;
using Newtonsoft.Json;
using System.Net.Http;
using Timer.DAL.Extensions;

namespace gtdtimer.Services
{
    public class LogInService : ILogInService
    {
        private IUnitOfWork userManager;
        private static readonly HttpClient Client = new HttpClient();
        public JWTManager jwtManager;

        public LogInService(IUnitOfWork userManager)
        {
            this.userManager = userManager;
            this.jwtManager = new JWTManager();
        }
        public LogInService(IUnitOfWork userManager, JWTManager jwtManager)
        {
            this.userManager = userManager;
            this.jwtManager = jwtManager;
        }

        public string CreateToken(LoginDTO model)
        {
            var user = userManager.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new LoginFailedException();
            }

            if (!userManager.UserManager.CheckPasswordAsync(user, model.Password).Result)
            {
                throw new LoginFailedException();
            }

            var userRoles = userManager.UserManager.GetRolesAsync(user.Id).Result;
            var token = jwtManager.GenerateToken(user, userRoles);

            return token;
        }

        public string CreateTokenWithGoogle(SocialAuthDTO accessToken)
        {
            string jwt = GetTokenBase<GoogleAuthUserData>(accessToken, Constants.GoogleResponsePath);

            return jwt;
        }

        public string CreateTokenWithFacebook(SocialAuthDTO accessToken)
        {
            string jwt = GetTokenBase<FacebookAuthUserData>(accessToken, Constants.FacebookResponsePath);

            return jwt;
        }

        private string GetTokenBase<T>(SocialAuthDTO accessToken, string path) where T : BaseAuthUserData
        {
            string userInfoResponse = null;

            try
            {
                userInfoResponse = Client.GetStringAsync($"{path}{accessToken.AccessToken}").Result;
            }
            catch (AggregateException ex) when (ex.InnerException.GetType().Equals(typeof(HttpRequestException)))
            {
                throw new Exception("Access token is invalid");
            }

            var userInfo = JsonConvert.DeserializeObject<T>(userInfoResponse);

            User user = userManager.UserManager.FindByEmailAsync(userInfo.Email).Result;

            if (user == null)
            {
                user = userInfo.ToUser();
                var result = userManager.UserManager.CreateAsync(user, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8)).Result;

                if (!result.Succeeded)
                {
                    throw new Exception("Couldn't add/create new user"); ;
                }
            }

            var userRoles = userManager.UserManager.GetRolesAsync(user.Id).Result;
            string jwt = jwtManager.GenerateToken(user, userRoles);

            return jwt;
        }

    }
}
