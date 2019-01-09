using Common.Exceptions;
using Common.ModelsDTO;
using gtdtimer.Services;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTierTests
{
    class LoginServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IApplicationUserManager<User, int>> userManager;
        private LogInService subject;
        private const string TokenPartTest = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userManager = new Mock<IApplicationUserManager<User, int>>();
            subject = new LogInService(unitOfWork.Object);
        }

        [Test]
        public void CreateToken()
        {
            LoginDTO model = new LoginDTO { Email = "test@gmail.com", Password = "12345" };
            User user = new User() { PasswordHash = "12345", Email = "test@gmail.com" };

            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);

            var actual = subject.CreateToken(model);

            Assert.AreEqual(actual.Contains(TokenPartTest), true);
        }

        [Test]
        public void LoginTest_Throws_UserNotFoundException()
        {
            LoginDTO model = new LoginDTO { Email = "", Password = "12345" };
            User user = null;

            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);

            var ex = Assert.Throws<UserNotFoundException>(() => subject.CreateToken(model));

            Assert.That(ex.Message, Is.EqualTo(Common.Constant.Constants.ErrorMessageUserNotFound));
        }

        [Test]
        public void LoginTest_Throws_IncorrectPasswordException()
        {
            LoginDTO model = new LoginDTO { Email = "", Password = "12345" };
            User user = new User() { PasswordHash = "1234" };

            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);

            var ex = Assert.Throws<LoginFailedException>(() => subject.CreateToken(model));

            Assert.That(ex.Message, Is.EqualTo(Common.Constant.Constants.ErrorMessageIncorrectPassword));
        }
    }
}
