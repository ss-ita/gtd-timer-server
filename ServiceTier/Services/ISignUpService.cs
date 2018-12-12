using gtdtimer.Timer.DAL.Entities;
using gtdtimer.Timer.DTO;

namespace ServiceTier.Services
{
    public interface ISignUpService : IBaseService
    {
        void AddUser(UserDTO model);
        User GetUserById(int id);
    }
}
