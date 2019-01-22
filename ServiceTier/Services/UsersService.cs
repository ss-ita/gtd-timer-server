using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Common.Constant;
using Common.Exceptions;
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
            unitOfWork.UserManager.AddToRoleAsync(user.Id, Constants.UserRole).GetAwaiter().GetResult();
            unitOfWork.Save();
        }

        public void UpdatePassword(int id, UpdatePasswordDTO model)
        {
            User user = Get(id);
            if (!unitOfWork.UserManager.CheckPasswordAsync(user, model.PasswordOld).Result)
            {
                throw new PasswordMismatchException();
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

        /// <summary>
        /// Add to roles of user
        /// </summary>
        /// <param name="model"></param>
        public void AddToRole(RoleDTO model)
        {
            var user = unitOfWork.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var roles = unitOfWork.UserManager.GetRolesAsync(user.Id).Result;

            foreach (string role in roles)
            {
                if (role == model.Role)
                {
                    throw new Exception("Role already exist");
                }
            }

            unitOfWork.UserManager.AddToRoleAsync(user.Id, model.Role).GetAwaiter();
        }

        /// <summary>
        /// Remove from roles of user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        public void RemoveFromRoles(string email, string role)
        {
            var user = unitOfWork.UserManager.FindByEmailAsync(email).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            unitOfWork.UserManager.RemoveFromRoleAsync(user.Id, role).GetAwaiter();
        }

        /// <summary>
        /// Get ysers emails
        /// </summary>
        /// <returns></returns>
        public IList<string> GetUsersEmails(string roleName)
        {
            var emailsList = unitOfWork.UserManager.GetAllEmails(roleName).Result;

            return emailsList;
        }

        /// <summary>
        /// Delete user by email
        /// </summary>
        /// <param name="emeil"></param>
        public void DeleteUserByEmail(string emeil)
        {
            User user = unitOfWork.UserManager.FindByEmailAsync(emeil).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            unitOfWork.UserManager.DeleteAsync(user).GetAwaiter().GetResult();

            unitOfWork.Save();
        }

        /// <summary>
        /// Get roles of user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<string> GetRolesOfUser(int id)
        {
            var roles = unitOfWork.UserManager.GetRolesAsync(id).Result;

            return roles;
        }
    }
}
