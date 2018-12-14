using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

using Common.Constant;
using gtdtimer.Controllers;
using Common.Model;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;
using System.Net;

namespace LoginControllerTests
{
    [TestFixture]
    public class LoginControllerTest
    {
        private Mock<ApplicationUserManager> userManegerMock;
        private LogInController controller;

        public LoginControllerTest()
        {
            userManegerMock = new Mock<ApplicationUserManager>();
            controller = new LogInController(userManegerMock.Object);
        }

        [Test]
        public async Task LoginTest_ReturnsBadRequest_WhenModelNotCorect()
        {
            var loginModel = new LoginModel { Email = "sfd", Password = "sdf" };

            var result = await controller.LoginAsync(loginModel);
            StatusCodeResult status = new StatusCodeResult((int)HttpStatusCode.Unauthorized);

            Assert.AreEqual(status, result);
        }

        [Test]
        public async Task LoginTest_ReturnsBadRequest_WhenModelCorect()
        {
            var loginModel = new LoginModel { Email = Constants.CorectEmail, Password = Constants.CorectPassword };

            var result = await controller.LoginAsync(loginModel);
            StatusCodeResult status = new StatusCodeResult((int)HttpStatusCode.OK);

            Assert.AreEqual(status, result);
        }
    }
}

