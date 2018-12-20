using Common.Exceptions;
using Common.Extensions;
using gtdtimer.ModelsDTO;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTier.Services
{
    public class SignUpService : BaseService, ISignUpService
    {
        public SignUpService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void AddUser(UserDTO model)
        {
            if (UserExists(model))
            {
                throw new UserAlreadyExistsException();
            }
             
           User user = model.ToUser();
           unitOfWork.UserManager.CreateAsync(user).GetAwaiter().GetResult();

            unitOfWork.Save();
        }

        public User GetUserById(int id)
        {
            var user = unitOfWork.UserManager.FindByIdAsync(id).Result;

            return user;
        }

        private bool UserExists(UserDTO model)
        {
            var userToFind = unitOfWork.UserManager.FindByEmailAsync(model.Email).Result;

            return userToFind != null;
        }
    }
}