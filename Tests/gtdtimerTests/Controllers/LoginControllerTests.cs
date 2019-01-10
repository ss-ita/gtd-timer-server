using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

using Common.Constant;
using gtdtimer.Controllers;
using Common.ModelsDTO;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;
using System.Net;
using ServiceTier.Services;
using Common.Exceptions;

namespace LoginControllerTests
{
    [TestFixture]
    public class LoginControllerTest
    {
        [TestFixture]
        public class SignUpControllerTests
        {
            private Mock<ILogInService> logInService;

            private LogInController subject;

            [SetUp]
            public void Setup()
            {
                logInService = new Mock<ILogInService>();
                subject = new LogInController(logInService.Object);
            }

            [Test]
            public void LoginTest_ReturnsOkRequest_WhenModelCorect()
            {
                LoginDTO model = new LoginDTO() { Email = Constants.CorectEmail, Password = Constants.CorectPassword };

                var actual = (OkObjectResult)subject.Login(model);

                Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            }

            [Test]
            public void LoginTest_Throws_UserNotFoundException()
            {
                LoginDTO model = new LoginDTO();

                logInService.Setup(_ => _.CreateToken(model)).Throws(new UserNotFoundException());

                var ex = Assert.Throws<UserNotFoundException>(() => subject.Login(model));

                Assert.That(ex.Message, Is.EqualTo("User does not Exist!"));
            }

            [Test]
            public void GoogleLoginTest_ReturnsOkRequest_WhenTokenCorect()
            {
                SocialAuthDTO model = new SocialAuthDTO() { AccessToken = Constants.CorectEmail };

                var actual = (OkObjectResult)subject.GoogleLogin(model);

                Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            }

            [Test]
            public void FacebookLoginTest_ReturnsOkRequest_WhenTokenCorect()
            {
                SocialAuthDTO model = new SocialAuthDTO() { AccessToken = Constants.CorectEmail };

                var actual = (OkObjectResult)subject.FacebookLogin(model);

                Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            }
        }
    }
}

