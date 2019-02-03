//-----------------------------------------------------------------------
// <copyright file="ITokenService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

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
        /// Method for getting token by user id
        /// </summary>
        /// <param name="userEmail">Email of user which token to return</param>
        /// <returns>token object</returns>
        Token GetTokenByUserEmail(string userEmail);

        /// <summary>
        /// Method for sending user verification email
        /// </summary>
        /// <param name="user">user instance</param>
        void SendUserVerificationToken(User user);
    }
}
