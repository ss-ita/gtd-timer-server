//-----------------------------------------------------------------------
// <copyright file="LoginControllerTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using GtdCommon.Constant;
using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdTimer.Controllers;
using GtdServiceTier.Services;

namespace GtdTimerTests.Controllers
{
    [TestFixture]
    public class LoginControllerTests
    {
        [TestFixture]
        public class SignUpControllerTests
        {
            private Mock<ILogInService> logInService;

            private LogInController subject;

            /// <summary>
            /// Method which is called immediately in each test run
            /// </summary>
            [SetUp]
            public void Setup()
            {
                logInService = new Mock<ILogInService>();
                subject = new LogInController(logInService.Object);
            }

            /// <summary>
            /// Ok response test
            /// </summary>
            [Test]
            public void LoginTest_ReturnsOkRequest_WhenModelCorect()
            {
                LoginDto model = new LoginDto() { Email = Constants.CorectEmail, Password = Constants.CorectPassword };

                var actual = (OkObjectResult)subject.Login(model);

                Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            }

            /// <summary>
            /// User not found exception test
            /// </summary>
            [Test]
            public void LoginTest_Throws_UserNotFoundException()
            {
                LoginDto model = new LoginDto();

                logInService.Setup(_ => _.CreateToken(model)).Throws(new UserNotFoundException());

                var ex = Assert.Throws<UserNotFoundException>(() => subject.Login(model));

                Assert.That(ex.Message, Is.EqualTo("User does not Exist!"));
            }

            /// <summary>
            /// Ok response with google test
            /// </summary>
            [Test]
            public void GoogleLoginTest_ReturnsOkRequest_WhenTokenCorect()
            {
                SocialAuthDto model = new SocialAuthDto() { AccessToken = Constants.CorectEmail };

                var actual = (OkObjectResult)subject.GoogleLogin(model);

                Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            }

            /// <summary>
            /// Ok response facebook test
            /// </summary>
            [Test]
            public void FacebookLoginTest_ReturnsOkRequest_WhenTokenCorect()
            {
                SocialAuthDto model = new SocialAuthDto() { AccessToken = Constants.CorectEmail };

                var actual = (OkObjectResult)subject.FacebookLogin(model);

                Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            }
        }
    }
}