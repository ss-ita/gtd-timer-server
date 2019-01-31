//-----------------------------------------------------------------------
// <copyright file="IApplicationUserManager.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using GtdTimerDAL.Entities;
using Microsoft.AspNet.Identity;

namespace GtdTimerDAL.Repositories
{
    /// <summary>
    /// interface for application user manager class
    /// </summary>
    /// <typeparam name="TUser"> generic user instance</typeparam>
    /// <typeparam name="TKey"> generic key instance</typeparam>
    public interface IApplicationUserManager<TUser, TKey>
    {
        /// <summary>
        /// Gets or sets user roles table
        /// </summary>
        IRepository<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets users table
        /// </summary>
        IRepository<User> Users { get; set; }

        /// <summary>
        /// Gets or sets roles table
        /// </summary>
        IRepository<Role> Roles { get; set; }

        /// <summary>
        /// Method for creating a user
        /// </summary>
        /// <param name="user"> generic user </param>
        /// <returns> new user </returns>
        Task<IdentityResult> CreateAsync(TUser user);

        /// <summary>
        /// Method for creating a user
        /// </summary>
        /// <param name="user"> generic user </param>
        /// <param name="password"> password of account </param>
        /// <returns> new user </returns>
        Task<IdentityResult> CreateAsync(TUser user, string password);

        /// <summary>
        /// Method for updating a user which already exists
        /// </summary>
        /// <param name="user">generic user</param>
        /// <returns> updated user </returns>
        Task<IdentityResult> UpdateAsync(TUser user);

        /// <summary>
        /// Method for deleting a user
        /// </summary>
        /// <param name="user">generic user</param>
        /// <returns> result of deleting </returns>
        Task<IdentityResult> DeleteAsync(TUser user);

        /// <summary>
        /// Method for finding a user by id
        /// </summary>
        /// <param name="userId"> id of user </param>
        /// <returns> returns user </returns>
        Task<TUser> FindByIdAsync(TKey userId);

        /// <summary>
        /// Method for finding a user by name
        /// </summary>
        /// <param name="userName"> user name </param>
        /// <returns> returns user </returns>
        Task<TUser> FindByNameAsync(string userName);

        /// <summary>
        /// Method for changing email
        /// </summary>
        /// <param name="userId"> id of user </param>
        /// <param name="email"> user email </param>
        /// <returns> result of changing email</returns>
        Task<IdentityResult> SetEmailAsync(TKey userId, string email);

        /// <summary>
        /// Method for finding a user by email
        /// </summary>
        /// <param name="email"> user email </param>
        /// <returns> returns user </returns>
        Task<TUser> FindByEmailAsync(string email);

        /// <summary>
        /// Method for obtaining a user role
        /// </summary>
        /// <param name="key"> key of role </param>
        /// <returns> list of roles </returns>
        Task<IList<string>> GetRolesAsync(TKey key);

        /// <summary>
        /// Method for adding role for user
        /// </summary>
        /// <param name="key"> key of role </param>
        /// <param name="roleName"> role name </param>
        /// <returns> result of adding role to user </returns>
        Task<IdentityResult> AddToRoleAsync(TKey key, string roleName);

        /// <summary>
        /// Method for removing role for user
        /// </summary>
        /// <param name="key"> key of role </param>
        /// <param name="roleName"> role name </param>
        /// <returns> result of deleting role from user </returns>
        Task<IdentityResult> RemoveFromRoleAsync(TKey key, string roleName);

        /// <summary>
        /// Method for checking user role
        /// </summary>
        /// <param name="key"> key of role </param>
        /// <param name="roleName"> role name </param>
        /// <returns>result of checking is user in role </returns>
        Task<bool> IsInRoleAsync(TKey key, string roleName);

        /// <summary>
        /// Method for checking password
        /// </summary>
        /// <param name="user">generic user</param>
        /// <param name="password"> password of account </param>
        /// <returns> result of checking password </returns>
        Task<bool> CheckPasswordAsync(TUser user, string password);

        /// <summary>
        /// Method for checking password
        /// </summary>
        /// <param name="userId"> id of user </param>
        /// <param name="currentPassword"> password of account </param>
        /// <param name="newPassword"> new password of account </param>
        /// <returns> result of checking password </returns>
        Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword);

        /// <summary>
        /// Get the email confirmation token for the user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>confirmation token</returns>
        Task<string> GenerateEmailConfirmationTokenAsync(TKey userId);
    }
}
