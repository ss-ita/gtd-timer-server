//-----------------------------------------------------------------------
// <copyright file="TokenService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Web;

using GtdCommon.Email;
using GtdCommon.IoC;
using GtdTimerDAL.Entities;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements ITokenService interface
    /// </summary>
    public class TokenService : BaseService, ITokenService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        public TokenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateToken(Token token)
        {
            UnitOfWork.Tokens.Create(token);
            UnitOfWork.Save();
        }

        public Token GetTokenByUserEmail(string userEmail)
        {
            var userToken = UnitOfWork.Tokens.GetAllEntitiesByFilter(token => token.UserEmail == userEmail)
                .Select(token => token);

            return userToken.FirstOrDefault();
        }

        public void SendUserVerificationToken(User user)
        {
            var emailVerificationCode = UnitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user.Id).GetAwaiter().GetResult();

            Token token = new Token() { UserEmail = user.Email, TokenValue = emailVerificationCode, TokenCreationTime = DateTime.Now };

            CreateToken(token);

            var host = Environment.GetEnvironmentVariable("AzureCors") ?? IoCContainer.Configuration["Origins"];

            var confirmationUrl = $"{host}/confirm-email/{user.Email}/{HttpUtility.UrlEncode(emailVerificationCode)}";

            GtdTimerEmailSender.SendUserVerificationEmailAsync(user.UserName, user.Email, confirmationUrl).GetAwaiter().GetResult();
        }
    }
}
