﻿using Common.Exceptions;
using Common.ModelsDTO;
using Timer.DAL.Extensions;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTier.Services
{
    public class UsersService : BaseService, IUsersService
    {
        public UsersService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User Get(int id)
        {
            var user = unitOfWork.UserManager.FindByIdAsync(id).Result;

            return user;
        }

        public void Create(UserDTO model)
        {
            if (UserExists(model))
            {
                throw new UserAlreadyExistsException();
            }

            User user = model.ToUser();
            unitOfWork.UserManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
            unitOfWork.Save();
        }

        public void Update(int id, UpdatePasswordDTO model)
        {
            User user = Get(id);
            if (!unitOfWork.UserManager.CheckPasswordAsync(user, model.PasswordOld).Result)
            {
                throw new IncorrectPasswordException();
            }

            var result = unitOfWork.UserManager.ChangePasswordAsync(id, model.PasswordOld, model.PasswordNew).Result;
            unitOfWork.Save();
        }

        public void Delete(int id)
        {
            User user = Get(id);
            unitOfWork.UserManager.DeleteAsync(user).GetAwaiter().GetResult();
            unitOfWork.Save();
        }

        private bool UserExists(UserDTO model)
        {
            var userToFind = unitOfWork.UserManager.FindByEmailAsync(model.Email).Result;

            return userToFind != null;
        }
    }
}
