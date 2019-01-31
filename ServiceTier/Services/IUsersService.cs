//-----------------------------------------------------------------------
// <copyright file="IUsersService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// Interface for users service
    /// </summary>
    public interface IUsersService : IBaseService
    {
        /// <summary>
        /// Method for getting user by id
        /// </summary>
        /// <param name="id">id of chosen user</param>
        /// <returns>return user with chosen id</returns>
        User Get(int id);

        /// <summary>
        /// Method for creating user
        /// </summary>
        /// <param name="model">user model</param>
        void Create(UserDto model);

        /// <summary>
        /// Method for verification email confirmation token
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="emailToken">user confirmation token</param>
        void VerifyToken(string userId, string emailToken);

        /// <summary>
        /// Method for sending user verification email
        /// </summary>
        /// <param name="user">user</param>
        void SendUserVerificationEmail(User user);

        /// <summary>
        /// Method for updating user password
        /// </summary>
        /// <param name="id">id of chosen user</param>
        /// <param name="model">password confirmation model</param>
        void UpdatePassword(int id, UpdatePasswordDto model);

        /// <summary>
        /// Method for deleting user
        /// </summary>
        /// <param name="id">id of chosen user</param>
        void Delete(int id);

        /// <summary>
        /// Add to roles of user
        /// </summary>
        /// <param name="model">role model</param>
        void AddToRole(RoleDto model);

        /// <summary>
        /// Remove from roles of user
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="role">user role</param>
        void RemoveFromRoles(string email, string role);

        /// <summary>
        /// Get users emails
        /// </summary>
        /// <param name="roleName">user role name</param>
        /// <param name="userId">user id</param>
        /// <returns>emails of all users</returns>
        IList<string> GetUsersEmails(string roleName, int userId);

        /// <summary>
        /// Delete user by email
        /// </summary>
        /// <param name="email">user email</param>
        void DeleteUserByEmail(string email);

        /// <summary>
        /// Get roles of user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>list of roles of chosen user</returns>
        IList<string> GetRolesOfUser(int id);
    }
}
