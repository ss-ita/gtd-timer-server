//-----------------------------------------------------------------------
// <copyright file="UsersService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Web;
using System.Collections.Generic;

using GtdCommon.Constant;
using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdCommon.Email;
using GtdTimerDAL.Extensions;
using GtdTimerDAL.Entities;
using GtdTimerDAL.UnitOfWork;
using System.Linq;


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
            var result = UnitOfWork.UserManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
            UnitOfWork.UserManager.AddToRoleAsync(user.Id, Constants.UserRole).GetAwaiter().GetResult();
            UnitOfWork.Save();
            if (result.Succeeded)
            {
                var emailVerificationCode = UnitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(user.Id).GetAwaiter().GetResult();
                var confirmationUrl = $"http://localhost:4200/confirm-email/{HttpUtility.UrlEncode(user.Id.ToString())}/{HttpUtility.UrlEncode(emailVerificationCode)}";
                GtdTimerEmailSender.SendUserVerificationEmailAsync(user.UserName, user.Email, confirmationUrl).GetAwaiter().GetResult();
            }
        }

        public string Verify(string userId, string emailToken)
        {
            var user = UnitOfWork.UserManager.FindByIdAsync(Convert.ToInt32(userId)).GetAwaiter().GetResult();
            if (user == null)
                throw new UserNotFoundException();

            var result = UnitOfWork.UserManager.ConfirmEmailAsync(user.Id, emailToken).GetAwaiter().GetResult();

            if (result.Succeeded)
                return "Email Verified :)";

            return "Invalid Email Verification Token :(";
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

        public void AddToRole(RoleDto model)
        {
            var user = UnitOfWork.UserManager.FindByEmailAsync(model.Email).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var roles = UnitOfWork.UserManager.GetRolesAsync(user.Id).Result;

            foreach (string role in roles)
            {
                if (role == model.Role)
                {
                    throw new Exception("Role already exist");
                }
            }

            UnitOfWork.UserManager.AddToRoleAsync(user.Id, model.Role).GetAwaiter();
        }

        public void RemoveFromRoles(string email, string role)
        {
            var user = UnitOfWork.UserManager.FindByEmailAsync(email).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            UnitOfWork.UserManager.RemoveFromRoleAsync(user.Id, role).GetAwaiter();
        }

        public IList<string> GetUsersEmails(string roleName)
        {
            Role role = UnitOfWork.Roles.GetAllEntities().Select(userRole => userRole).Where(userRole => userRole.Name == roleName).FirstOrDefault();
            IList<string> emails = new List<string>();

            if (role.Name == Constants.UserRole)
            {
                var userList = UnitOfWork.Users.GetAllEntities().Select(user => user).ToList();
                var userRoleList = UnitOfWork.UserRoles.GetAllEntities().Select(userRole => userRole).ToList();
                emails = (from user in userList
                          join userRole in userRoleList
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == role.Id
                          select user.Email).ToList<string>().Except(
                         (from user in userList
                          join userRole in userRoleList
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == 2 || userRole.RoleId == 3
                          select user.Email).ToList<string>()).ToList();
            }

            if (role.Name == GtdCommon.Constant.Constants.AdminRole)
            {
                var userList = UnitOfWork.Users.GetAllEntities().Select(user => user).ToList();
                var userRoleList = UnitOfWork.UserRoles.GetAllEntities().Select(userRole => userRole).ToList();
                emails = (from user in userList
                          join userRole in userRoleList
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == role.Id
                          select user.Email).ToList<string>();
            }

            return emails;
        }

        public void DeleteUserByEmail(string emeil)
        {
            User user = UnitOfWork.UserManager.FindByEmailAsync(emeil).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            UnitOfWork.UserManager.DeleteAsync(user).GetAwaiter().GetResult();

            UnitOfWork.Save();
        }

        public IList<string> GetRolesOfUser(int id)
        {
            var roles = UnitOfWork.UserManager.GetRolesAsync(id).Result;

            return roles;
        }

        private bool UserExists(UserDto model)
        {
            var userToFind = UnitOfWork.UserManager.FindByEmailAsync(model.Email).Result;

            return userToFind != null;
        }
    }
}
