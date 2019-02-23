//-----------------------------------------------------------------------
// <copyright file="UsersService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;

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
        /// Instance of token service
        /// </summary>
        private readonly ITokenService tokenService;

        /// <summary>
        /// Instance of preset service
        /// </summary>
        private readonly IPresetService presetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        /// <param name="tokenService">instance of token service</param>
        public UsersService(IUnitOfWork unitOfWork , ITokenService tokenService, IPresetService presetService) : base(unitOfWork)
        {
            this.tokenService = tokenService;
            this.presetService = presetService;
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
            tokenService.SendUserVerificationToken(user);
        }

        public void VerifyEmailToken(string userEmail, string emailToken)
        {
            var token = tokenService.GetTokenByUserEmail(userEmail, TokenType.EmailVerification);

            if (token == null)
            {
                throw new InvalidTokenException("Token has expired");
            }

            if (DateTime.Now > token.TokenExpirationTime)
            {
                throw new InvalidTokenException("Token has expired , resend verification email?");
            }

            var user = UnitOfWork.UserManager.FindByEmailAsync(userEmail).GetAwaiter().GetResult();

            var result = token.TokenValue == emailToken ? true : false;

            if (result)
            {
                user.EmailConfirmed = true;
                tokenService.DeleteTokenByUserEmail(userEmail, TokenType.EmailVerification);
                UnitOfWork.UserManager.UpdateAsync(user).GetAwaiter().GetResult();
                UnitOfWork.Save();
            }
            else throw new InvalidTokenException("Token has expired");
        }

        public void ResendVerificationEmail(string userEmail)
        {
            var user = UnitOfWork.UserManager.FindByEmailAsync(userEmail).GetAwaiter().GetResult();

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            tokenService.DeleteTokenByUserEmail(userEmail, TokenType.EmailVerification);
            tokenService.SendUserVerificationToken(user);
        }

        public void SendPasswordRecoveryEmail(string userEmail)
        {
            var user = UnitOfWork.UserManager.FindByEmailAsync(userEmail).GetAwaiter().GetResult();

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var token = tokenService.GetTokenByUserEmail(userEmail, TokenType.PasswordRecovery);

            if (token != null)
            {
                tokenService.DeleteTokenByUserEmail(userEmail, TokenType.PasswordRecovery);
            }

            tokenService.SendUserRecoveryToken(user);
        }

        public void VerifyPasswordRecoveryToken(string userEmail, string recoveryToken)
        {
            var token = tokenService.GetTokenByUserEmail(userEmail, TokenType.PasswordRecovery);

            if (token == null || DateTime.Now > token.TokenExpirationTime)
            {
                throw new InvalidTokenException("Token has expired");
            }

            var user = UnitOfWork.UserManager.FindByEmailAsync(userEmail).GetAwaiter().GetResult();

            var result = token.TokenValue == recoveryToken ? true : false;

            if (result)
            {
                tokenService.DeleteTokenByUserEmail(userEmail, TokenType.PasswordRecovery);
            }
            else throw new InvalidTokenException("Token has expired");
        }

        public void ResetPassword(string userEmail, string newPassword)
        {
            var user = UnitOfWork.UserManager.FindByEmailAsync(userEmail).GetAwaiter().GetResult();

            user.PasswordHash = UnitOfWork.UserManager.PasswordHasher.HashPassword(newPassword);

            UnitOfWork.UserManager.UpdateAsync(user).GetAwaiter().GetResult();
            UnitOfWork.Save();
        }

        public void UpdatePassword(int id, UpdatePasswordDto model)
        {
            User user = Get(id);
            if (user.EmailConfirmed == false)
            {
                throw new AccessDeniedException("Forbidden! Confirm your email address first!");
            }
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
            if (user.EmailConfirmed == false)
            {
                throw new AccessDeniedException("Forbidden! Confirm your email address first!");
            }
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

        public IList<string> GetUsersEmails(string roleName, int userId)
        {
            Role role = UnitOfWork.UserManager.Roles.GetAllEntities().Select(userRole => userRole).Where(userRole => userRole.Name == roleName).FirstOrDefault();
            IList<string> emails = new List<string>();
            var userEmail = UnitOfWork.UserManager.FindByIdAsync(userId).GetAwaiter().GetResult().Email;

            if (role.Name == Constants.UserRole)
            {
                var userList = UnitOfWork.UserManager.Users.GetAllEntities().Select(user => user).ToList();
                var userRoleList = UnitOfWork.UserManager.UserRoles.GetAllEntities().Select(userRole => userRole).ToList();
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
                var userList = UnitOfWork.UserManager.Users.GetAllEntities().Select(user => user).ToList();
                var userRoleList = UnitOfWork.UserManager.UserRoles.GetAllEntities().Select(userRole => userRole).ToList();
                emails = (from user in userList
                          join userRole in userRoleList
                          on user.Id equals userRole.UserId
                          where userRole.RoleId == role.Id
                          select user.Email).ToList<string>();
            }

            emails.Remove(userEmail);

            return emails;
        }

        public void DeleteUserByEmail(string emeil)
        {
            User user = UnitOfWork.UserManager.FindByEmailAsync(emeil).Result;

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            presetService.DeleteAllPresetsByUserId(user.Id);
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
