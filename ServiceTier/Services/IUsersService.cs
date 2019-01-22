//-----------------------------------------------------------------------
// <copyright file="IUsersService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// Method for assigning a role for user
        /// </summary>
        /// <param name="model">role model</param>
        /// <returns>result of adding into a role</returns>
        Task AdDtoRoleAsync(RoleDto model);

        /// <summary>
        /// Method for deleting a role from user
        /// </summary>
        /// <param name="model">role model</param>
        /// <returns>result of removing role from user </returns>
        Task RemoveFromRolesAsync(RoleDto model);

        /// <summary>
        /// Method for getting all user emails
        /// </summary>
        /// <returns>list of emails of all users</returns>
        Task<IList<string>> GetUsersEmailsAsync();
    }
}
