//-----------------------------------------------------------------------
// <copyright file="UserIdentityService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using GtdCommon.Constant;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements i user identity service interface
    /// </summary>
    public class UserIdentityService : IUserIdentityService
    {
        /// <summary>
        /// instance of http context accessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdentityService" /> class.
        /// </summary>
        /// <param name="httpContextAccessor">instance of http context accessor</param>
        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var identity = (ClaimsIdentity)this.httpContextAccessor.HttpContext.User.Identity;

            return int.Parse(identity.FindFirst(Constants.ClaimUserId).Value);
        }
    }
}
