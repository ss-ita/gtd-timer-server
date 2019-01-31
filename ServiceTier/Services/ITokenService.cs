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
        /// <param name="userId">Id of user which token to return</param>
        /// <returns>token object</returns>
        Token GetTokenByUserId(int userId);
    }
}
