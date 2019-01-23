//-----------------------------------------------------------------------
// <copyright file="LoginServiceTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdServiceTier.Services;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTierTests
{
    public class LoginServiceTests
    {
        private readonly string jwtTokenTest = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0OTciLCJzdWIiOiJzYXNoYXR5bW9zaGNodWswN0BnbWFpbC5jb20iLCJqdGkiOiJiMjA1NTYzZS01ZDZmLTQ3MjktYmZmNC1hNWNlYjRjYjI2YmIiLCJleHAiOjE1NDcxMDg1MDIsImlzcyI6IlRva2VuczpJc3N1ZXIiLCJhdWQiOiJUb2tlbnM6QXVkaWVuY2UifQ.yjE7zzCNUk8aMGkIkkmZk2IFEs-b8CvpT6r-OLLI2GA";
        private readonly string errorMessageInvaliDtoken = "Access token is invalid";
        private readonly string facebookInvaliDtoken = "EAAMnrYZCCs68BAFLwsiOAXBi95piRosjnvU5BeZCkfxeTzS7lD5PCZAZBxXNogkGAkAw2N3arefWlXq7e6qb2HAKgyXj9Fh8y3szccJZBZAFPCYns6NYN2WjqkBnCLrWr3n7gbc07qspNZB8e6";
        private readonly string googleInvaliDtoken = "ya29.GlyMBnfx9gxRcYk99RO-LQx0AA2tbtHWL2kkkcQ_dn-XS9g1E89FX3sIlQ3Xs4PufhO9X2rR2k7stPlfZIGIyNhua7QicadcCTNAq-v9KTdjLG2i9zF7qtda6wX4Gg";
        private readonly string errorMessageLoginFailed = "Incorrect credentials entered";
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IApplicationUserManager<User, int>> userManager;
        private Mock<JWTManager> jwtManager;
        private LogInService subject;
        private User user = new User() { PasswordHash = "12345", Email = "sashatymoshchuk07@gmail.com" };

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userManager = new Mock<IApplicationUserManager<User, int>>();
            jwtManager = new Mock<JWTManager>();
            subject = new LogInService(unitOfWork.Object, jwtManager.Object);
        }

        /// <summary>
        /// Test for creating access token
        /// </summary>
        [Test]
        public void CreateToken()
        {
            LoginDto model = new LoginDto { Email = "sashatymoshchuk07@gmail.com", Password = "12345" };
            IList<string> roles = new List<string>() { "Admin", "User" };

            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            unitOfWork.Setup(_ => _.UserManager.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager.CheckPasswordAsync(user, model.Password)).ReturnsAsync(model.Password == user.PasswordHash);
            unitOfWork.Setup(_ => _.UserManager.GetRolesAsync(user.Id)).ReturnsAsync(roles);
            jwtManager.Setup(_ => _.GenerateToken(user, roles)).Returns(jwtTokenTest);

            var actual = subject.CreateToken(model);

            Assert.AreEqual(actual, jwtTokenTest);
        }

        /// <summary>
        /// Test for creating access token with facebook bad response
        /// </summary>
        [Test]
        public void CreateTokenWithFacebook_Throws_InvaliDtokenException()
        {
            SocialAuthDto token = new SocialAuthDto { AccessToken = facebookInvaliDtoken };
            IList<string> roles = new List<string>();

            userManager.Setup(_ => _.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            unitOfWork.Setup(_ => _.UserManager.GetRolesAsync(user.Id)).ReturnsAsync(roles);
            jwtManager.Setup(_ => _.GenerateToken(user, roles)).Returns(jwtTokenTest);

            var ex = Assert.Throws<Exception>(() => subject.CreateTokenWithFacebook(token));

            Assert.That(ex.Message, Is.EqualTo(errorMessageInvaliDtoken));
        }

        /// <summary>
        /// Test for creating access token with google bad response
        /// </summary>
        [Test]
        public void CreateTokenWithGoogle_Throws_InvaliDtokenException()
        {
            SocialAuthDto token = new SocialAuthDto { AccessToken = googleInvaliDtoken };
            IList<string> roles = new List<string>();

            userManager.Setup(_ => _.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            unitOfWork.Setup(_ => _.UserManager.GetRolesAsync(user.Id)).ReturnsAsync(roles);
            jwtManager.Setup(_ => _.GenerateToken(user, roles)).Returns(jwtTokenTest);

            var ex = Assert.Throws<Exception>(() => subject.CreateTokenWithGoogle(token));

            Assert.That(ex.Message, Is.EqualTo(errorMessageInvaliDtoken));
        }

        /// <summary>
        /// Test for logging in failed
        /// </summary>
        [Test]
        public void LoginTest_UserNotFound_Throws_LoginFailedException()
        {
            LoginDto model = new LoginDto { Email = string.Empty, Password = "12345" };
            User user = null;

            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);

            var ex = Assert.Throws<LoginFailedException>(() => subject.CreateToken(model));

            Assert.That(ex.Message, Is.EqualTo(errorMessageLoginFailed));
        }
    }
}
