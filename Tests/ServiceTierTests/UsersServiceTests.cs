//-----------------------------------------------------------------------
// <copyright file="UsersServiceTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;

using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdServiceTier.Services;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;
using GtdCommon.Constant;

namespace GtdServiceTierTests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private const string UserEmail = "User email";
        private const string UserToken = "User Token";
        private const string NewPassword = "New password";
        private const string HashPassword = "Hash password";
        private readonly IApplicationUserManager<User, int> userManager;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<ITokenService> tokenService;
        private Mock<IPresetService> presetService;
        private UsersService subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            tokenService = new Mock<ITokenService>();
            presetService = new Mock<IPresetService>();
            subject = new UsersService(unitOfWork.Object, tokenService.Object, presetService.Object);
        }

        /// <summary>
        /// get user by id test
        /// </summary>
        [Test]
        public void Get()
        {
            int userID = 1;
            User user = new User();

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByIdAsync(userID)).ReturnsAsync(user);

            var actual = subject.Get(userID);

            Assert.AreSame(actual, user);
        }

        /// <summary>
        /// Create async user test
        /// </summary>
        [Test]
        public void Create()
        {
            UserDto model = new UserDto { Email = string.Empty };
            var userManager = new Mock<IApplicationUserManager<User, int>>();
            var tokens = new Mock<IRepository<Token>>();
            var identity = new IdentityResult();
            User user = new User();

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);
            userManager.Setup(_ => _.CreateAsync(user, model.Password)).ReturnsAsync(identity);
            userManager.Setup(_ => _.AddToRoleAsync(user.Id, GtdCommon.Constant.Constants.UserRole)).ReturnsAsync(identity);
            tokenService.Setup(_ => _.SendUserVerificationToken(user));

            subject.Create(model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
            userManager.Verify(_ => _.CreateAsync(It.IsAny<User>(), model.Password), Times.Once);
            userManager.Verify(_ => _.AddToRoleAsync(user.Id, GtdCommon.Constant.Constants.UserRole), Times.Once);
        }

        /// <summary>
        /// Verify email token test
        /// </summary>
        [Test]
        public void VerifyEmailToken()
        {
            Token token = new Token();
            User user = new User();

            tokenService.Setup(_ => _.GetTokenByUserEmail(UserEmail, TokenType.EmailVerification)).Returns(token);
            tokenService.Setup(_ => _.DeleteTokenByUserEmail(UserEmail, TokenType.EmailVerification));
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(UserEmail)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.UpdateAsync(user));

            unitOfWork.Verify(_ => _.Save(), Times.Never);
            var exception = Assert.Throws<InvalidTokenException>(() => subject.VerifyEmailToken(UserEmail, UserToken));

            Assert.That(exception.Message, Is.EqualTo("Token has expired , resend verification email?"));
        }

        /// <summary>
        /// Resend verification email token test
        /// </summary>
        [Test]
        public void ResendVerificationEmail()
        {
            Token token = new Token();
            User user = new User();

            tokenService.Setup(_ => _.SendUserVerificationToken(user));
            tokenService.Setup(_ => _.DeleteTokenByUserEmail(UserEmail, TokenType.EmailVerification));
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(UserEmail)).ReturnsAsync(user);

            subject.ResendVerificationEmail(UserEmail);

            unitOfWork.Verify(_ => _.Save(), Times.Never);
        }

        /// <summary>
        /// Send password recovery email test
        /// </summary>
        [Test]
        public void SendPasswordRecoveryEmail()
        {
            Token token = new Token();
            User user = new User();

            tokenService.Setup(_ => _.GetTokenByUserEmail(UserEmail, TokenType.PasswordRecovery)).Returns(token);
            tokenService.Setup(_ => _.DeleteTokenByUserEmail(UserEmail, TokenType.PasswordRecovery));
            tokenService.Setup(_ => _.SendUserRecoveryToken(user));
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(UserEmail)).ReturnsAsync(user);

            subject.SendPasswordRecoveryEmail(UserEmail);

            unitOfWork.Verify(_ => _.Save(), Times.Never);
        }

        /// <summary>
        /// Verify password recovery token test
        /// </summary>
        [Test]
        public void VerifyPasswordRecoveryToken()
        {
            Token token = new Token();
            User user = new User();

            tokenService.Setup(_ => _.GetTokenByUserEmail(UserEmail, TokenType.PasswordRecovery)).Returns(token);
            tokenService.Setup(_ => _.DeleteTokenByUserEmail(UserEmail, TokenType.PasswordRecovery));
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(UserEmail)).ReturnsAsync(user);

            unitOfWork.Verify(_ => _.Save(), Times.Never);
            var exception = Assert.Throws<InvalidTokenException>(() => subject.VerifyPasswordRecoveryToken(UserEmail, UserToken));

            Assert.That(exception.Message, Is.EqualTo("Token has expired"));
        }

        /// <summary>
        /// Reset password test
        /// </summary>
        [Test]
        public void ResetPassword()
        {
            User user = new User();

            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(UserEmail)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.PasswordHasher.HashPassword(NewPassword)).Returns(HashPassword);
            unitOfWork.Setup(_ => _.UserManager.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            subject.ResetPassword(UserEmail, NewPassword);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// User Already Exists Exception test
        /// </summary>
        [Test]
        public void Create_Throws_UserAlreadyExistsException()
        {
            UserDto model = new UserDto { Email = string.Empty };
            var userManager = new Mock<IApplicationUserManager<User, int>>();
            User user = new User();

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            var ex = Assert.Throws<UserAlreadyExistsException>(() => subject.Create(model));

            Assert.That(ex.Message, Is.EqualTo("User with such email address already exists"));
        }

        /// <summary>
        /// update password test
        /// </summary>
        [Test]
        public void UpdatePassword()
        {
            int userId = 1;
            string password = "password";
            UpdatePasswordDto model = new UpdatePasswordDto { PasswordOld = password };
            User user = new User { PasswordHash = password, EmailConfirmed = true };

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByIdAsync(userId)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.CheckPasswordAsync(user, model.PasswordOld)).ReturnsAsync(true);

            subject.UpdatePassword(userId, model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Update Throws Password Mismatch Exception test
        /// </summary>
        [Test]
        public void UpdatePassword_Throws_PasswordMismatchException()
        {
            int userId = 1;
            UpdatePasswordDto model = new UpdatePasswordDto();
            User user = new User { PasswordHash = "password", EmailConfirmed = true };

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByIdAsync(userId)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.CheckPasswordAsync(user, model.PasswordOld)).ReturnsAsync(false);

            var ex = Assert.Throws<PasswordMismatchException>(() => subject.UpdatePassword(userId, model));

            Assert.That(ex.Message, Is.EqualTo("Incorrect password entered"));
        }

        /// <summary>
        /// Delete user test
        /// </summary>
        [Test]
        public void Delete()
        {
            int userId = 1;
            User user = new User() { EmailConfirmed = true };

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByIdAsync(userId)).ReturnsAsync(user);

            subject.Delete(userId);

            unitOfWork.Verify(_ => _.UserManager.DeleteAsync(user), Times.Once);
            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Dispose test
        /// </summary>
        [Test]
        public void Dispose()
        {
            subject.Dispose();

            unitOfWork.Verify(_ => _.Dispose(), Times.Once);
        }

        /// <summary>
        /// Add role test
        /// </summary>
        [Test]
        public void AddRoleTest_ReturnsOkRequest_WhenModelCorect()
        {
            RoleDto model = new RoleDto() { Email = GtdCommon.Constant.Constants.CorectEmail, Role = GtdCommon.Constant.Constants.AdminRole };
            User user = new User();
            var roles = new List<string>();
            var identity = new IdentityResult();
            
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.GetRolesAsync(user.Id)).ReturnsAsync(roles);
            unitOfWork.Setup(_ => _.UserManager.AddToRoleAsync(user.Id, model.Role)).ReturnsAsync(identity);

            subject.AddToRole(model);

            unitOfWork.Verify(_ => _.UserManager.AddToRoleAsync(user.Id, model.Role), Times.Once);
        }

        /// <summary>
        /// Remove role test
        /// </summary>
        [Test]
        public void RemoveRoleTest_ReturnsOkRequest_WhenModelCorect()
        {
            RoleDto model = new RoleDto() { Email = GtdCommon.Constant.Constants.CorectEmail, Role = GtdCommon.Constant.Constants.AdminRole };
            User user = new User();
            var identity = new IdentityResult();

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.RemoveFromRoleAsync(user.Id, model.Role)).ReturnsAsync(identity);

            subject.RemoveFromRoles(model.Email, model.Role);

            unitOfWork.Verify(_ => _.UserManager.RemoveFromRoleAsync(user.Id, model.Role), Times.Once);
        }

        /// <summary>
        /// get all emails test
        /// </summary>
        [Test]
        public void GetAllEmailTest_ReturnsOkRequest()
        {
            int userId = 1;
            var roles = new List<Role>();
            var users = new List<User>();
            var userRoles = new List<UserRole>();
            Role role = new Role();
            User user = new User();
            role.Name = "Admin";
            roles.Add(role);

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.Roles.GetAllEntities()).Returns(roles);
            unitOfWork.Setup(_ => _.UserManager.Users.GetAllEntities()).Returns(users);
            unitOfWork.Setup(_ => _.UserManager.UserRoles.GetAllEntities()).Returns(userRoles);
            unitOfWork.Setup(_ => _.UserManager.FindByIdAsync(userId)).ReturnsAsync(user);

            var actual = subject.GetUsersEmails(GtdCommon.Constant.Constants.AdminRole, userId);

            unitOfWork.Verify(_ => _.UserManager.Roles.GetAllEntities(), Times.Once);
            unitOfWork.Verify(_ => _.UserManager.UserRoles.GetAllEntities(), Times.Once);
            unitOfWork.Verify(_ => _.UserManager.Users.GetAllEntities(), Times.Once);
        }

        /// <summary>
        /// test to check user role
        /// </summary>
        [Test]
        public void GetRolesOfUserTest_ReturnsOkRequest()
        {
            var roles = new List<string>();
            int id = 1;

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.GetRolesAsync(id)).ReturnsAsync(roles);

            var actual = subject.GetRolesOfUser(id);

            Assert.AreSame(actual, roles);
        }

        /// <summary>
        /// test to check deleting of user
        /// </summary>
        [Test]
        public void DeleteUserByEmailTest_ReturnsOkRequest_WhenEmailCorect()
        {
            string email = string.Empty;
            User user = new User();
            var identity = new IdentityResult();

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.DeleteAsync(user)).ReturnsAsync(identity);

            subject.DeleteUserByEmail(email);

            unitOfWork.Verify(_ => _.UserManager.DeleteAsync(user), Times.Once);
        }

        /// <summary>
        /// test to che if exception is thrown
        /// </summary>
        [Test]
        public void RemoveRole_Throws_UserNotFoundException()
        {
            RoleDto model = new RoleDto() { Email = GtdCommon.Constant.Constants.CorectEmail, Role = GtdCommon.Constant.Constants.AdminRole };
            User user = null;

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            var ex = Assert.Throws<UserNotFoundException>(() => subject.RemoveFromRoles(model.Email, model.Role));

            Assert.That(ex.Message, Is.EqualTo("User does not Exist!"));
        }

        /// <summary>
        /// test to check if adding role throws exception
        /// </summary>
        [Test]
        public void AddRole_Throws_UserNotFoundException()
        {
            RoleDto model = new RoleDto() { Email = GtdCommon.Constant.Constants.CorectEmail, Role = GtdCommon.Constant.Constants.AdminRole };
            User user = null;

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            var ex = Assert.Throws<UserNotFoundException>(() => subject.AddToRole(model));

            Assert.That(ex.Message, Is.EqualTo("User does not Exist!"));
        }

        /// <summary>
        /// Test to check if deleting 
        /// </summary>
        [Test]
        public void DeleteUserByEmail_Throws_UserNotFoundException()
        {
            RoleDto model = new RoleDto() { Email = GtdCommon.Constant.Constants.CorectEmail, Role = GtdCommon.Constant.Constants.AdminRole };
            User user = null;

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager);
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            var ex = Assert.Throws<UserNotFoundException>(() => subject.DeleteUserByEmail(model.Email));

            Assert.That(ex.Message, Is.EqualTo("User does not Exist!"));
        }
    }
}