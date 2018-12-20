using Common.Exceptions;
using gtdtimer.ModelsDTO;
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
    public class SignUpServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;

        private SignUpService subject;

        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new SignUpService(unitOfWork.Object);
        }

        [Test]
        public void GetUserById()
        {
            int userID = 1;
            User user = new User();
            var userRepository = new Mock<IUserStore<User, int>>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
            userRepository.Setup(_ => _.FindByIdAsync(userID)).ReturnsAsync(user);

            var actual = subject.GetUserById(userID);

            Assert.AreSame(actual, user);
        }

        [Test]
        public void AddUser()
        {
            UserDTO model = new UserDTO { Email = "" };
            var userRepository = new Mock<IUserEmailStore<User, int>>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
            userRepository.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync((User)null);

            subject.AddUser(model);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void AddUser_Throws_UserAlreadyExistsException()
        {
            UserDTO model = new UserDTO { Email = "" };
            User user = new User();
            var userRepository = new Mock<IUserEmailStore<User, int>>();

            unitOfWork.Setup(_ => _.UserManager).Returns(new ApplicationUserManager(userRepository.Object));
            userRepository.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);

            var ex = Assert.Throws<UserAlreadyExistsException>(() => subject.AddUser(model));

            Assert.That(ex.Message, Is.EqualTo("User with such email address already exists"));
        }

        [Test]
        public void Dispose()
        {
            subject.Dispose();

            unitOfWork.Verify(_ => _.Dispose(), Times.Once);
        }
    }
}