﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Common.Constant;
using Common.Exceptions;
using Common.ModelsDTO;
using gtdtimer.Attributes;
using ServiceTier.Services;

namespace gtdtimer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController: ControllerBase
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
            usersService.UpdatePassword(userId, model);

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

        /// <summary>
        /// Add role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpPost("AddRole")]
        public IActionResult AddToRole([FromBody]RoleDTO model)
        {
            usersService.AddToRole(model);

            return Ok();
        }

        /// <summary>
        /// Remove role
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Authorize(Roles = Constants.SuperAdminRole)]
        [HttpDelete("RemoveRole/{email}/{role}")]
        public IActionResult RemoveFromRoles(string email, string role)
        {
            usersService.RemoveFromRoles(email, role);

            return Ok();
        }

        /// <summary>
        /// Get users emails
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("GetUsersEmails")]
        public IActionResult GetUsersEmails()
        {
            var emailsList = usersService.GetUsersEmails();

            return Ok(emailsList);
        }

        /// <summary>
        /// Delete user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpDelete("DeleteUserByEmail/{email}")]
        public ActionResult DeleteUserByEmail(string email)
        {
            usersService.DeleteUserByEmail(email);

            return Ok();
        }

        /// <summary>
        /// Get roles of user
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRolesOfUser")]
        public ActionResult GetRolesOfUser()
        {
            var userId = userIdentityService.GetUserId();
            var roles = usersService.GetRolesOfUser(userId);

            return Ok(roles);
        }
    }
}
