using System.Collections.Generic;
using System.Threading.Tasks;

using Common.ModelsDTO;
using Timer.DAL.Timer.DAL.Entities;

namespace ServiceTier.Services
{
    public interface IUsersService : IBaseService
    {
        User Get(int id);
        void Create(UserDTO model);
        void UpdatePassword(int id, UpdatePasswordDTO model);
        void Delete(int id);

        /// <summary>
        /// Add to roles of user
        /// </summary>
        /// <param name="model"></param>
        void AddToRole(RoleDTO model);

        /// <summary>
        /// Remove from roles of user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        void RemoveFromRoles(string email, string role);

        /// <summary>
        /// Get ysers emails
        /// </summary>
        /// <returns></returns>
        IList<string> GetUsersEmails();

        /// <summary>
        /// Delete user by email
        /// </summary>
        /// <param name="emeil"></param>
        void DeleteUserByEmail(string emeil);

        /// <summary>
        /// Get roles of user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<string> GetRolesOfUser(int id);
    }
}
