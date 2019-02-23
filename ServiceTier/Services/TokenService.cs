//-----------------------------------------------------------------------
// <copyright file="TokenService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Web;
using GtdCommon.Constant;
using GtdCommon.Email;
using GtdCommon.Exceptions;
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

        public void DeleteTokenByUserEmail(string email, TokenType tokenType)
        {
            var tokenToDelete = GetTokenByUserEmail(email, tokenType);
            if (tokenToDelete != null)
            {
                UnitOfWork.Tokens.Delete(tokenToDelete);
                UnitOfWork.Save();
            }
            else
            {
                throw new InvalidTokenException("Token has expired!");
            }
        }

        public Token GetTokenByUserEmail(string userEmail , TokenType tokenType)
        {
            var userToken = UnitOfWork.Tokens.GetAllEntitiesByFilter(token => token.UserEmail == userEmail && token.TokenType == tokenType)
                .Select(token => token);

            return userToken.FirstOrDefault();
        }

        public void SendUserVerificationToken(User user)
        {
            var emailVerificationCode = UnitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user.Id).GetAwaiter().GetResult();

            Token token = new Token()
            {
                UserEmail = user.Email,
                TokenValue = emailVerificationCode,
                TokenCreationTime = DateTime.Now,
                TokenExpirationTime = DateTime.Now.AddDays(Constants.EmailTokenExpiration),
                TokenType = TokenType.EmailVerification
            };

            CreateToken(token);

            var host = Environment.GetEnvironmentVariable("AzureCors") ?? IoCContainer.Configuration["Origins"];

            var confirmationUrl = $"{host}/{Constants.ConfirmEmail}/{user.Email}/{HttpUtility.UrlEncode(emailVerificationCode)}";

            SendUserVerificationEmail(user, confirmationUrl);
        }

        public void SendUserRecoveryToken(User user)
        {
            var passwordRecoveryCode = UnitOfWork.UserManager.GeneratePasswordResetTokenAsync(user.Id).GetAwaiter().GetResult();

            Token token = new Token()
            {
                UserEmail = user.Email,
                TokenValue = passwordRecoveryCode,
                TokenCreationTime = DateTime.Now,
                TokenExpirationTime = DateTime.Now.AddHours(Constants.PasswordRecoveryTokenExpiration),
                TokenType = TokenType.PasswordRecovery
            };

            CreateToken(token);

            var host = Environment.GetEnvironmentVariable("AzureCors") ?? IoCContainer.Configuration["Origins"];

            var passwordRecoveryUrl = $"{host}/{Constants.PasswordRecovery}/{user.Email}/{HttpUtility.UrlEncode(passwordRecoveryCode)}";

            SendPasswordRecoveryEmail(user, passwordRecoveryUrl);
        }

        public void SendUserVerificationEmail(User user, string confirmationUrl)
        {
            GtdTimerEmailSender.SendUserVerificationEmailAsync(user.UserName, user.Email, confirmationUrl,
                Constants.VerifyEmailButton, Constants.VerifyEmailTitle, Constants.VerifyEmailMessage).GetAwaiter().GetResult();
        }

        public void SendPasswordRecoveryEmail(User user, string passwordRecoveryUrl)
        {
            GtdTimerEmailSender.SendUserVerificationEmailAsync(user.UserName, user.Email, passwordRecoveryUrl,
                Constants.PasswordResetButton , Constants.PasswordResetTitle, Constants.PasswordResetMessage).GetAwaiter().GetResult();
        }
    }
}
