using System.Linq;

using Common.Exceptions;
using Common.Extensions;
using gtdtimer.ModelsDTO;
using gtdtimer.Timer.DAL.Entities;
using gtdtimer.Timer.DAL.UnitOfWork;

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

            unitOfWork.Users.Create(user);
            unitOfWork.Save();
        }

        public User GetUserById(int id)
        {
            var user = unitOfWork.Users.GetByID(id);

            return user;
        }

        private bool UserExists(UserDTO model)
        {
            var userExist = unitOfWork.Users.GetAll().Any(u => u.Email == model.Email);

            return userExist;
        }
    }
}