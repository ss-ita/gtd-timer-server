//-----------------------------------------------------------------------
// <copyright file="TimerDalTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;

using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;

namespace GtdTimerDalTests
{
    [TestFixture]
    public class TimerDalTests
    {
        private Mock<TimerContext> mockContext;
        private Mock<IUserStore<User, int>> userRepository;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IApplicationUserManager<User, int>> userManager;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<TimerContext>();
            unitOfWork = new Mock<IUnitOfWork>();
            userRepository = new Mock<IUserStore<User, int>>();
            userManager = new Mock<IApplicationUserManager<User, int>>();
        }

        /// <summary>
        /// create user test
        /// </summary>
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

        /// <summary>
        /// delete user test
        /// </summary>
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

        /// <summary>
        /// update user test
        /// </summary>
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