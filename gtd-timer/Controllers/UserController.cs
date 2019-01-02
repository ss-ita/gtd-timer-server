using Common.Exceptions;
using Common.ModelsDTO;
using gtdtimer.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceTier.Services;

namespace gtdtimer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserIdentityService userIdentityService;
        private readonly IUsersService usersService;

        public UserController(IUserIdentityService userIdentityService, IUsersService usersService)
        {
            this.userIdentityService = userIdentityService;
            this.usersService = usersService;
        }

        /// <summary>
        /// Retrieve current user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var userId = userIdentityService.GetUserId();
            var user = usersService.Get(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return Ok(user);
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="model">The DTO model of User entity</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateModel]
        [HttpPost]
        public ActionResult Post([FromBody]UserDTO model)
        {
            usersService.Create(model);

            return Ok();
        }

        /// <summary>
        /// Update current user password.
        /// </summary>
        /// <param name="model">The DTO model of Password entity</param>
        /// <returns></returns>
        [ValidateModel]
        [HttpPut]
        public ActionResult Put([FromBody]UpdatePasswordDTO model)
        {
            var userId = userIdentityService.GetUserId();
            usersService.Update(userId, model);

            return Ok();
        }

        /// <summary>
        /// Delete current user.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult Delete()
        {
            var userId = userIdentityService.GetUserId();
            usersService.Delete(userId);

            return Ok();
        }
    }
}
