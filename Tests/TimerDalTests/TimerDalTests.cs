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
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IApplicationUserManager<User, int>> userManeger;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<TimerContext>();
            unitOfWork = new Mock<IUnitOfWork>();
            userManeger = new Mock<IApplicationUserManager<User, int>>();
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

            unitOfWork.Setup(obj => obj.UserManager).Returns(userManeger.Object);
            unitOfWork.Setup(obj => obj.UserManager.FindByEmailAsync(user.Email)).ReturnsAsync((User)null);

            unitOfWork.Object.UserManager.CreateAsync(user);
            unitOfWork.Verify(obj => obj.UserManager.CreateAsync(user), Times.Once);
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

            unitOfWork.Setup(obj => obj.UserManager).Returns(userManeger.Object);
            unitOfWork.Setup(obj => obj.UserManager.DeleteAsync(user));

            unitOfWork.Object.UserManager.DeleteAsync(user);
            unitOfWork.Verify(obj => obj.UserManager.DeleteAsync(user), Times.Once);
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

            unitOfWork.Setup(obj => obj.UserManager).Returns(userManeger.Object);
            unitOfWork.Setup(obj => obj.UserManager.UpdateAsync(user));

            unitOfWork.Object.UserManager.UpdateAsync(user);
            unitOfWork.Verify(obj => obj.UserManager.UpdateAsync(user), Times.Once);
        }
    }
}