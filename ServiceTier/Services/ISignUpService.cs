using gtdtimer.ModelsDTO;

namespace ServiceTier.Services
{
    public interface ISignUpService : IBaseService
    {
        void AddUser(UserDTO model);
    }
}