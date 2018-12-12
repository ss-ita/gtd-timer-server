using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

using Common.Constant;
using gtdtimer.Controllers;
using Common.Model;
using gtdtimer.Timer.DAL.Entities;
using System.Net;

namespace LoginControllerTests
{
    [TestFixture]
    public class LoginControllerTest
    {
        private Mock<ApplicationUserManager<User>> userManegerMock;
        private LoginController controller;

        public LoginControllerTest()
        {
            userManegerMock = new Mock<ApplicationUserManager<User>>();
            controller = new LoginController(userManegerMock.Object);
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

