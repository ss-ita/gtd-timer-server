using Common.ModelsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timer.DAL.Timer.DAL.Entities;

namespace ServiceTier.Services
{
    public interface IUsersService : IBaseService
    {
        User Get(int id);
        Task CreateAsync(UserDTO model);
        void Update(int id, UpdatePasswordDTO model);
        void Delete(int id);
        Task AddToRoleAsync(RoleDTO model);
        Task RemoveFromRolesAsync(RoleDTO model);
        Task<IList<string>> GetUsersEmailsAsync();
    }
}
