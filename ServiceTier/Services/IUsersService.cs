using Common.ModelsDTO;
using Timer.DAL.Timer.DAL.Entities;

namespace ServiceTier.Services
{
    public interface IUsersService : IBaseService
    {
        User Get(int id);
        void Create(UserDTO model);
        void Update(int id, UpdatePasswordDTO model);
        void Delete(int id);
    }
}
