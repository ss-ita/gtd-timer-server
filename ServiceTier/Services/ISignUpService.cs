using gtdtimer.ModelsDTO;
using gtdtimer.Timer.DAL.Entities;

namespace ServiceTier.Services
{
    public interface ISignUpService : IBaseService
    {
        void AddUser(UserDTO model);
        User GetUserById(int id);
    }
}