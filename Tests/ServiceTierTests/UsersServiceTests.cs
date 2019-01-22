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

namespace GtdServiceTierTests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;

        private UsersService subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new UsersService(unitOfWork.Object);
        }

        /// <summary>
        /// get user by id test
        /// </summary>
        [Test]
        public void Get()
        {
            int userID = 1;
            User user = new User();
            var userRepository = new Mock<IUserStore<User, int>>();
            var timerContext = new Mock<TimerContext>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            userRepository.Setup(_ => _.FindByIdAsync(userID)).ReturnsAsync(user);

            var actual = subject.Get(userID);

            Assert.AreSame(actual, user);
        }

        /// <summary>
        /// Create async user test
        /// </summary>
        /// <returns>response of creating user</returns>
        [Test]
        public async System.Threading.Tasks.Task CreateAsync()
        {
            UserDto model = new UserDto { Email = string.Empty };
            var userManager = new Mock<IApplicationUserManager<User, int>>();
            var timerContext = new Mock<TimerContext>();
            var identity = new IdentityResult();
            User user = new User();

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);
            userManager.Setup(_ => _.CreateAsync(user, model.Password)).ReturnsAsync(identity);
            userManager.Setup(_ => _.AddToRoleAsync(user.Id, GtdCommon.Constant.Constants.UserRole)).ReturnsAsync(identity);

            subject.Create(model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
            userManager.Verify(_ => _.CreateAsync(It.IsAny<User>(), model.Password), Times.Once);
            userManager.Verify(_ => _.AddToRoleAsync(user.Id, GtdCommon.Constant.Constants.UserRole), Times.Once);
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
        public void Update()
        {
            int userId = 1;
            string password = "password";
            UpdatePasswordDto model = new UpdatePasswordDto { PasswordOld = password };
            User user = new User { PasswordHash = password };

            var timerContext = new Mock<TimerContext>();
            var userRepository = new Mock<IUserStore<User, int>>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.FindByIdAsync(userId)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.CheckPasswordAsync(user, model.PasswordOld)).ReturnsAsync(true);

            subject.UpdatePassword(userId, model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Update Throws Password Mismatch Exception test
        /// </summary>
        [Test]
        public void Update_Throws_PasswordMismatchException()
        {
            int userId = 1;
            UpdatePasswordDto model = new UpdatePasswordDto();
            User user = new User { PasswordHash = "password" };

            var userRepository = new Mock<IUserStore<User, int>>();
            var timerContext = new Mock<TimerContext>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
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
            User user = new User();
            var userRepository = new Mock<IUserEmailStore<User, int>>();
            var timerContext = new Mock<TimerContext>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            userRepository.Setup(_ => _.FindByIdAsync(userId)).ReturnsAsync(user);

            subject.Delete(userId);

            userRepository.Verify(_ => _.DeleteAsync(user), Times.Once);
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
        public void AddRole()
        {
            RoleDto model = new RoleDto() { Email = GtdCommon.Constant.Constants.CorectEmail, Role = GtdCommon.Constant.Constants.AdminRole };
            User user = new User();
            var roles = new List<string>();

            var timerContext = new Mock<TimerContext>();
            var userRepository = new Mock<IUserStore<User, int>>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.GetRolesAsync(user.Id)).ReturnsAsync(roles);
            unitOfWork.Setup(_ => _.UserManager.AddToRoleAsync(user.Id, model.Role));

            subject.AdDtoRoleAsync(model);

            unitOfWork.Verify(_ => _.UserManager.AddToRoleAsync(user.Id, model.Role), Times.Once);
        }

        /// <summary>
        /// Remove role test
        /// </summary>
        /// <returns>result of removing role</returns>
        [Test]
        public async System.Threading.Tasks.Task RemoveRole()
        {
            RoleDto model = new RoleDto() { Email = GtdCommon.Constant.Constants.CorectEmail, Role = GtdCommon.Constant.Constants.AdminRole };
            User user = new User();

            var timerContext = new Mock<TimerContext>();
            var userRepository = new Mock<IUserStore<User, int>>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.RemoveFromRoleAsync(user.Id, model.Role));

            await subject.RemoveFromRolesAsync(model);

            unitOfWork.Verify(_ => _.UserManager.RemoveFromRoleAsync(user.Id, model.Role), Times.Once);
        }

        /// <summary>
        /// get all emails test
        /// </summary>
        /// <returns>list of all emails </returns>
        [Test]
        public async System.Threading.Tasks.Task GetAllEmailTest_ReturnsOkRequestAsync()
        {
            var emails = new List<string>();

            var userRepository = new Mock<IUserEmailStore<User, int>>();
            var timerContext = new Mock<TimerContext>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.GetAllEmails()).ReturnsAsync(emails);

            var actual = await subject.GetUsersEmailsAsync();

            Assert.AreSame(actual, emails);
        }
    }
}