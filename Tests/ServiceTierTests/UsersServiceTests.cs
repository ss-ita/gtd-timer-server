using Microsoft.AspNet.Identity;
using System.Collections.Generic;

using Common.Exceptions;
using Common.ModelsDTO;
using Moq;
using NUnit.Framework;
using ServiceTier.Services;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTierTests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;

        private UsersService subject;

        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new UsersService(unitOfWork.Object);
        }

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

        [Test]
        public void Create()
        {
            UserDTO model = new UserDTO { Email = "" };
            var userManager = new Mock<IApplicationUserManager<User, int>>();
            var timerContext = new Mock<TimerContext>();
            var identity = new IdentityResult();
            User user = new User();

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);
            userManager.Setup(_ => _.CreateAsync(user, model.Password)).ReturnsAsync(identity);
            userManager.Setup(_ => _.AddToRoleAsync(user.Id, Common.Constant.Constants.UserRole)).ReturnsAsync(identity);

            subject.Create(model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
            userManager.Verify(_ => _.CreateAsync(It.IsAny<User>(), model.Password), Times.Once);
            userManager.Verify(_ => _.AddToRoleAsync(user.Id, Common.Constant.Constants.UserRole), Times.Once);
        }

        [Test]
        public void Create_Throws_UserAlreadyExistsException()
        {
            UserDTO model = new UserDTO { Email = "" };
            var userManager = new Mock<IApplicationUserManager<User, int>>();
            User user = new User();

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            var ex = Assert.Throws<UserAlreadyExistsException>(() => subject.Create(model));

            Assert.That(ex.Message, Is.EqualTo("User with such email address already exists"));
        }

        [Test]
        public void Update()
        {
            int userId = 1;
            string password = "password";
            UpdatePasswordDTO model = new UpdatePasswordDTO { PasswordOld = password };
            User user = new User { PasswordHash = password };

            var timerContext = new Mock<TimerContext>();
            var userRepository = new Mock<IUserStore<User, int>>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.FindByIdAsync(userId)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.CheckPasswordAsync(user, model.PasswordOld)).ReturnsAsync(true);

            subject.UpdatePassword(userId, model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void Update_Throws_PasswordMismatchException()
        {
            int userId = 1;
            UpdatePasswordDTO model = new UpdatePasswordDTO();
            User user = new User { PasswordHash = "password" };

            var userRepository = new Mock<IUserStore<User, int>>();
            var timerContext = new Mock<TimerContext>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.FindByIdAsync(userId)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.CheckPasswordAsync(user, model.PasswordOld)).ReturnsAsync(false);

            var ex = Assert.Throws<PasswordMismatchException>(() => subject.UpdatePassword(userId, model));

            Assert.That(ex.Message, Is.EqualTo("Incorrect password entered"));
        }

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

        [Test]
        public void Dispose()
        {
            subject.Dispose();

            unitOfWork.Verify(_ => _.Dispose(), Times.Once);
        }

        [Test]
        public void AddRoleTest_ReturnsOkRequest_WhenModelCorectAsync()
        {
            RoleDTO model = new RoleDTO() { Email = Common.Constant.Constants.CorectEmail, Role = Common.Constant.Constants.AdminRole };
            User user = new User();
            var roles = new List<string>();

            var timerContext = new Mock<TimerContext>();
            var userRepository = new Mock<IUserStore<User, int>>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.GetRolesAsync(user.Id)).ReturnsAsync(roles);
            unitOfWork.Setup(_ => _.UserManager.AddToRoleAsync(user.Id, model.Role));

            subject.AddToRoleAsync(model);

            unitOfWork.Verify(_ => _.UserManager.AddToRoleAsync(user.Id, model.Role), Times.Once);
        }

        [Test]
        public async System.Threading.Tasks.Task RemoveRoleTest_ReturnsOkRequest_WhenModelCorectAsync()
        {
            RoleDTO model = new RoleDTO() { Email = Common.Constant.Constants.CorectEmail, Role = Common.Constant.Constants.AdminRole };
            User user = new User();

            var timerContext = new Mock<TimerContext>();
            var userRepository = new Mock<IUserStore<User, int>>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.RemoveFromRoleAsync(user.Id, model.Role));

            subject.RemoveFromRolesAsync(model);

            unitOfWork.Verify(_ => _.UserManager.RemoveFromRoleAsync(user.Id, model.Role), Times.Once);
        }

        [Test]
        public async System.Threading.Tasks.Task GetAllEmailTest_ReturnsOkRequestAsync()
        {
            var emails = new List<string>();

            var userRepository = new Mock<IUserEmailStore<User, int>>();
            var timerContext = new Mock<TimerContext>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            unitOfWork.Setup(_ => _.UserManager.GetAllEmails()).ReturnsAsync(emails);

            var actual = await subject.GetUsersEmailsAsync();

            Assert.AreSame(actual, emails); ;
        }
    }
}