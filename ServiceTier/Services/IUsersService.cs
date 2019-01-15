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
        Task AddToRoleAsync(RoleDTO model);
        Task RemoveFromRolesAsync(RoleDTO model);
        Task<IList<string>> GetUsersEmailsAsync();
    }
}
