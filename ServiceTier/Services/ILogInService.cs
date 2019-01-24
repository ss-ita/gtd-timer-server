//-----------------------------------------------------------------------
// <copyright file="ILogInService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdCommon.ModelsDto;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// interface for log in service class
    /// </summary>
    public interface ILogInService
    {
        /// <summary>
        /// Method for creating a token
        /// </summary>
        /// <param name="model"> login model </param>
        /// <returns> new token </returns>
        string CreateToken(LoginDto model);

        /// <summary>
        /// Method for creating a token for authentication with google
        /// </summary>
        /// <param name="accessToken"> login model </param>
        /// <returns> new token </returns>
        string CreateTokenWithGoogle(SocialAuthDto accessToken);

        /// <summary>
        /// Method for creating a token for authentication with facebook
        /// </summary>
        /// <param name="accessToken"> login model </param>
        /// <returns> new token </returns>
        string CreateTokenWithFacebook(SocialAuthDto accessToken);
    }
}
