//-----------------------------------------------------------------------
// <copyright file="LogInService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Net.Http;
using Newtonsoft.Json;

using GtdCommon.Constant;
using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdTimerDAL.Extensions;
using GtdTimerDAL.Entities;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements i log in service
    /// </summary>
    public class LogInService : ILogInService
    {
        /// <summary>
        /// instance of http client
        /// </summary>
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// instance of token manager
        /// </summary>
        private readonly JWTManager jwtManager;

        /// <summary>
        /// instance of user manager
        /// </summary>
        private readonly IUnitOfWork userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInService" /> class.
        /// </summary>
        /// <param name="userManager">instance of user manager</param>
        public LogInService(IUnitOfWork userManager)
        {
            this.userManager = userManager;
            this.jwtManager = new JWTManager();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInService" /> class.
        /// </summary>
        /// <param name="userManager">instance of user manager</param>
        /// <param name="jwtManager">instance of token manager</param>
        public LogInService(IUnitOfWork userManager, JWTManager jwtManager)
        {
            this.userManager = userManager;
            this.jwtManager = jwtManager;
        }

        public string CreateToken(LoginDto model)
        {
            var user = this.userManager.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new LoginFailedException();
            }

            if (!this.userManager.UserManager.CheckPasswordAsync(user, model.Password).Result)
            {
                throw new LoginFailedException();
            }

            var userRoles = this.userManager.UserManager.GetRolesAsync(user.Id).Result;
            var token = this.jwtManager.GenerateToken(user, userRoles);

            return token;
        }

        public string CreateTokenWithEmail(string email)
        {
            var user = this.userManager.UserManager.FindByEmailAsync(email).Result;

            if (user == null)
            {
                throw new LoginFailedException();
            }

            var userRoles = this.userManager.UserManager.GetRolesAsync(user.Id).Result;
            var token = this.jwtManager.GenerateToken(user, userRoles);

            return token;
        }

        public string CreateTokenWithGoogle(SocialAuthDto accessToken)
        {
            string jwt = this.GetTokenBase<GoogleAuthUserData>(accessToken, Constants.GoogleResponsePath);

            return jwt;
        }

        public string CreateTokenWithFacebook(SocialAuthDto accessToken)
        {
            string jwt = this.GetTokenBase<FacebookAuthUserData>(accessToken, Constants.FacebookResponsePath);

            return jwt;
        }

        /// <summary>
        /// generates token with facebook or google
        /// </summary>
        /// <typeparam name="T">facebook or google type</typeparam>
        /// <param name="accessToken">token model</param>
        /// <param name="path">url for facebook or google</param>
        /// <returns>new token</returns>
        private string GetTokenBase<T>(SocialAuthDto accessToken, string path) where T : BaseAuthUserData
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
                userManager.UserManager.AddToRoleAsync(user.Id, Constants.UserRole).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception("Couldn't add/create new user");
                }
            }

            var userRoles = this.userManager.UserManager.GetRolesAsync(user.Id).Result;
            string jwt = this.jwtManager.GenerateToken(user, userRoles);

            return jwt;
        }
    }
}
