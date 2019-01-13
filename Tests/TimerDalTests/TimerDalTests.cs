using NUnit.Framework;
using Microsoft.AspNet.Identity;
using Moq;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private Mock<TimerContext> mockContext;
        private Mock<IUserStore<User, int>> userRepository;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IApplicationUserManager<User, int>> userManager;
        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<TimerContext>();
            unitOfWork = new Mock<IUnitOfWork>();
            userRepository = new Mock<IUserStore<User, int>>();
            userManager = new Mock<IApplicationUserManager<User, int>>();
        }

        [Test]
        public void CreateUser()
        {
            User user = new User()
            {
                UserName = "taraskat@gmail.com",
                Email = "taraskat@gmail.com",
                FirstName = "Taras",
                LastName = "Kataryna",
                PasswordHash = "qwertyQWERTY@@22"
            };
            var timerContext = new Mock<TimerContext>();

            unitOfWork.Setup(obj => obj.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));
            userManager.Setup(obj => obj.FindByEmailAsync(user.Email)).ReturnsAsync((User)null);

            unitOfWork.Object.UserManager.CreateAsync(user);
            userRepository.Verify(obj => obj.CreateAsync(user), Times.Once);
        }
        [Test]
        public void DeleteUser()
        {
            User user = new User()
            {
                UserName = "taraskat@gmail.com",
                Email = "taraskat@gmail.com",
                FirstName = "Taras",
                LastName = "Kataryna",
                PasswordHash = "qwertyQWERTY@@22"
            };
            var timerContext = new Mock<TimerContext>();

            unitOfWork.Setup(obj => obj.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));

            unitOfWork.Object.UserManager.DeleteAsync(user);
            userRepository.Verify(obj => obj.DeleteAsync(user), Times.Once);
        }
        [Test]
        public void UpdateUser()
        {
            User user = new User()
            {
                UserName = "taraskat@gmail.com",
                Email = "taraskat@gmail.com",
                FirstName = "Taras",
                LastName = "Kataryna",
                PasswordHash = "qwertyQWERTY@@22"
            };
            var timerContext = new Mock<TimerContext>();

            unitOfWork.Setup(obj => obj.UserManager).Returns(new ApplicationUserManager(userRepository.Object, timerContext.Object));

            unitOfWork.Object.UserManager.UpdateAsync(user);
            userRepository.Verify(obj => obj.UpdateAsync(user), Times.Once);
        }
    }
}