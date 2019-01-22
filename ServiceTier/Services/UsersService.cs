//-----------------------------------------------------------------------
// <copyright file="UsersService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using GtdCommon.Constant;
using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdTimerDAL.Extensions;
using GtdTimerDAL.Entities;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements i users service interface
    /// </summary>
    public class UsersService : BaseService, IUsersService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        public UsersService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public User Get(int id)
        {
            var user = UnitOfWork.UserManager.FindByIdAsync(id).Result;

            return user;
        }

        public void Create(UserDto model)
        {
            if (this.UserExists(model))
            {
                throw new UserAlreadyExistsException();
            }

            User user = model.ToUser();
            UnitOfWork.UserManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
            UnitOfWork.UserManager.AddToRoleAsync(user.Id, Constants.UserRole).GetAwaiter().GetResult();
            UnitOfWork.Save();
        }

        public void UpdatePassword(int id, UpdatePasswordDto model)
        {
            User user = Get(id);
            if (!UnitOfWork.UserManager.CheckPasswordAsync(user, model.PasswordOld).Result)
            {
                throw new PasswordMismatchException();
            }

            var result = UnitOfWork.UserManager.ChangePasswordAsync(id, model.PasswordOld, model.PasswordNew).Result;
            UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            User user = Get(id);
            UnitOfWork.UserManager.DeleteAsync(user).GetAwaiter().GetResult();
            UnitOfWork.Save();
        }

        public async Task AdDtoRoleAsync(RoleDto model)
        {
            var user = UnitOfWork.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var roles = await UnitOfWork.UserManager.GetRolesAsync(user.Id);

            foreach (string role in roles)
            {
                if (role == model.Role)
                {
                    throw new Exception("Role already exist");
                }
            }

            await UnitOfWork.UserManager.AddToRoleAsync(user.Id, model.Role);
        }

        public async Task RemoveFromRolesAsync(RoleDto model)
        {
            var user = UnitOfWork.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            await UnitOfWork.UserManager.RemoveFromRoleAsync(user.Id, model.Role);
        }

        public async Task<IList<string>> GetUsersEmailsAsync()
        {
            var emailsList = await UnitOfWork.UserManager.GetAllEmails();

            return emailsList;
        }

        private bool UserExists(UserDto model)
        {
            var userToFind = UnitOfWork.UserManager.FindByEmailAsync(model.Email).Result;

            return userToFind != null;
        }
    }
}
