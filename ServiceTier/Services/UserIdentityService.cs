using Common.Constant;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ServiceTier.Services
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var identity = (ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity;

            return int.Parse(identity.FindFirst(Constants.ClaimUserId).Value);
        }
    }
}
