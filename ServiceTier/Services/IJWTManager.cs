//-----------------------------------------------------------------------
// <copyright file="IJWTManager.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using GtdTimerDAL.Entities;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// Interface for Jason web token class
    /// </summary>
    public interface IJWTManager
    {
        /// <summary>
        /// Method for generating token
        /// </summary>
        /// <param name="user"> user model</param>
        /// <param name="userRoles"> list of user roles</param>
        /// <returns>new token</returns>
        string GenerateToken(User user, IList<string> userRoles);
    }
}