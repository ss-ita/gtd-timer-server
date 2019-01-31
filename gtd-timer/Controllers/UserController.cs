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
        /// Verify user email
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="emailToken">user confirmation token</param>
        /// <returns>result of token verification</returns>
        [AllowAnonymous]
        [HttpGet("Verify/{userId}/{emailToken}")]
        public ActionResult VerifyEmailAsync(string userId, string emailToken)
        {
            this.usersService.VerifyToken(userId, emailToken);

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
            usersService.AddToRole(model);

            return this.Ok();
        }

        /// <summary>
        /// Remove role
        /// </summary>
        /// <param name="email">email of chosen user</param>
        /// <param name="role">role of chosen user</param>
        /// <returns>result of deleting role from user</returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpDelete("RemoveRole/{email}/{role}")]
        public IActionResult RemoveFromRoles(string email, string role)
        {
            usersService.RemoveFromRoles(email, role);

            return Ok();
        }

        /// <summary>
        /// Get users emails
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>result of getting users emails</returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpDelete("DeleteUserByEmail/{email}")]
        public ActionResult DeleteUserByEmail(string email)
        {
            usersService.DeleteUserByEmail(email);

            return Ok();
        }

        /// <summary>
        /// Delete user by email
        /// </summary>
        /// <param name="roleName">user role</param>
        /// <returns>result of getting users emails</returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("GetUsersEmails/{roleName}")]
        public IActionResult GetUsersEmails(string roleName)
        {
            var userId = userIdentityService.GetUserId();
            var emailsList = usersService.GetUsersEmails(roleName, userId);

            return this.Ok(emailsList);
        }

        /// <summary>
        /// Get roles of user
        /// </summary>
        /// <returns>roles of chosen user</returns>
        [HttpGet("GetRolesOfUser")]
        public ActionResult GetRolesOfUser()
        {
            var userId = userIdentityService.GetUserId();
            var roles = usersService.GetRolesOfUser(userId);

            return Ok(roles);
        }
    }
}
