using Common.Exceptions;
using Common.ModelsDTO;
using Microsoft.AspNet.Identity;
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

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
            userRepository.Setup(_ => _.FindByIdAsync(userID)).ReturnsAsync(user);

            var actual = subject.Get(userID);

            Assert.AreSame(actual, user);
        }

        [Test]
        public void Create()
        {
            UserDTO model = new UserDTO { Email = "" };
            var userRepository = new Mock<IUserEmailStore<User, int>>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
            userRepository.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);

            subject.Create(model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void Create_Throws_UserAlreadyExistsException()
        {
            UserDTO model = new UserDTO { Email = "" };
            User user = new User();
            var userRepository = new Mock<IUserEmailStore<User, int>>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
            userRepository.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);

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

            var userRepository = new Mock<IUserEmailStore<User, int>>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
            userRepository.Setup(_ => _.FindByIdAsync(userId)).ReturnsAsync(user);

            subject.Update(userId, model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void Update_Throws_IncorrectPasswordException()
        {
            int userId = 1;
            UpdatePasswordDTO model = new UpdatePasswordDTO();
            User user = new User { PasswordHash = "password" };

            var userRepository = new Mock<IUserEmailStore<User, int>>();
            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
            userRepository.Setup(_ => _.FindByIdAsync(userId)).ReturnsAsync(user);

            var ex = Assert.Throws<IncorrectPasswordException>(() => subject.Update(userId, model));

            Assert.That(ex.Message, Is.EqualTo("Incorrect password entered"));
        }

        [Test]
        public void Delete()
        {
            int userId = 1;
            User user = new User();
            var userRepository = new Mock<IUserEmailStore<User, int>>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
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
    }
}