//-----------------------------------------------------------------------
// <copyright file="IUserIdentityService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdServiceTier.Services
{
    /// <summary>
    /// Interface for user identity class
    /// </summary>
    public interface IUserIdentityService
    {
        /// <summary>
        /// Method for getting id of current user
        /// </summary>
        /// <returns>id of current user</returns>
        int GetUserId();
    }
}
