//-----------------------------------------------------------------------
// <copyright file="UserController.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GtdCommon.Constant;
using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdTimer.Attributes;
using GtdServiceTier.Services;

namespace GtdTimer.Controllers
{
    /// <summary>
    /// class for user controller
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// instance of user identity service
        /// </summary>
        private readonly IUserIdentityService userIdentityService;

        /// <summary>
        /// instance of user service
        /// </summary>
        private readonly IUsersService usersService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="userIdentityService">instance of user identity service</param>
        /// <param name="usersService">instance of user service</param>
        public UserController(IUserIdentityService userIdentityService, IUsersService usersService)
        {
            this.userIdentityService = userIdentityService;
            this.usersService = usersService;
        }

        /// <summary>
        /// Retrieve current user.
        /// </summary>
        /// <returns>result of retrieving current user.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var userId = this.userIdentityService.GetUserId();
            var user = this.usersService.Get(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return this.Ok(user);
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="model">The Dto model of User entity</param>
        /// <returns>result of creating user.</returns>
        [AllowAnonymous]
        [ValidateModel]
        [HttpPost]
        public ActionResult Post([FromBody]UserDto model)
        {
            this.usersService.Create(model);

            return this.Ok();
        }

        /// <summary>
        /// Update current user password.
        /// </summary>
        /// <param name="model">The Dto model of Password entity</param>
        /// <returns>result of updating current user password.</returns>
        [ValidateModel]
        [HttpPut]
        public ActionResult Put([FromBody]UpdatePasswordDto model)
        {
            var userId = this.userIdentityService.GetUserId();
            this.usersService.UpdatePassword(userId, model);

            return this.Ok();
        }

        /// <summary>
        /// Delete current user.
        /// </summary>
        /// <returns>result of deleting current user.</returns>
        [HttpDelete]
        public ActionResult Delete()
        {
            var userId = this.userIdentityService.GetUserId();
            this.usersService.Delete(userId);

            return this.Ok();
        }

        /// <summary>
        /// Add role
        /// </summary>
        /// <param name="model">role model</param>
        /// <returns>result of adding role</returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpPost("AddRole")]
        public IActionResult AdDtoRole([FromBody]RoleDto model)
        {
            this.usersService.AdDtoRoleAsync(model);

            return this.Ok();
        }

        /// <summary>
        /// Remove Role
        /// </summary>
        /// <param name="model">role model</param>
        /// <returns>result of removing Role</returns>
        [Authorize(Roles = Constants.SuperAdminRole)]
        [HttpDelete("RemoveRole")]
        public IActionResult RemoveFromRoles([FromBody]RoleDto model)
        {
            this.usersService.RemoveFromRolesAsync(model);

            return this.Ok();
        }

        /// <summary>
        /// Get users emails
        /// </summary>
        /// <returns>result of getting users emails</returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("GetUsersEmails")]
        public async Task<IActionResult> GetUsersEmailsAsync()
        {
            var emailsList = await this.usersService.GetUsersEmailsAsync();

            return this.Ok(emailsList);
        }
    }
}
