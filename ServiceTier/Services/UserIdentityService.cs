//-----------------------------------------------------------------------
// <copyright file="UserIdentityService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

using GtdCommon.Constant;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// Class which implements user identity service interface
    /// </summary>
    public class UserIdentityService : IUserIdentityService
    {
        /// <summary>
        /// Instance of http context accessor
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
            var userId = GetUserId(httpContextAccessor.HttpContext.User.Identity);

            return userId;
        }

        public int GetUserId(IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;

            return int.Parse(claimsIdentity.FindFirst(Constants.ClaimUserId).Value);
        }
    }
}
