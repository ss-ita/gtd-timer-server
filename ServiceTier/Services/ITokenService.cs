//-----------------------------------------------------------------------
// <copyright file="ITokenService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdCommon.Constant;
using GtdTimerDAL.Entities;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// Interface for token service class
    /// </summary>
    public interface ITokenService : IBaseService
    {
        /// <summary>
        /// Method for creating a token
        /// </summary>
        /// <param name="token">token object</param>
        void CreateToken(Token token);

        /// <summary>
        /// Method for deleting a token by user email and token type
        /// </summary>
        /// <param name="userEmail">user email</param>
        /// <param name="tokenType">type of token</param>
        void DeleteTokenByUserEmail(string userEmail, TokenType tokenType);

        /// <summary>
        /// Method for getting token by user id
        /// </summary>
        /// <param name="userEmail">Email of user which token to return</param>
        /// <param name="tokenType">type of token</param> 
        /// <returns>token object</returns>
        Token GetTokenByUserEmail(string userEmail, TokenType tokenType);

        /// <summary>
        /// Method for sending user verification email to user
        /// </summary>
        /// <param name="user">user instance</param>
        void SendUserVerificationToken(User user);

        /// <summary>
        /// Method for sending user password recovery email to user
        /// </summary>
        /// <param name="user">user instance</param>
        void SendUserRecoveryToken(User user);
    }
}
