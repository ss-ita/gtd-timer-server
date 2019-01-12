using Common.Exceptions;
using Common.ModelsDTO;
using gtdtimer.Services;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using ServiceTier.Services;
using System;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTierTests
{
    class LoginServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IApplicationUserManager<User, int>> userManager;
        private Mock<JWTManager> jwtManager;
        private LogInService subject;
        User user = new User() { PasswordHash = "12345", Email = "sashatymoshchuk07@gmail.com" };
        private const string JwtTokenTest = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0OTciLCJzdWIiOiJzYXNoYXR5bW9zaGNodWswN0BnbWFpbC5jb20iLCJqdGkiOiJiMjA1NTYzZS01ZDZmLTQ3MjktYmZmNC1hNWNlYjRjYjI2YmIiLCJleHAiOjE1NDcxMDg1MDIsImlzcyI6IlRva2VuczpJc3N1ZXIiLCJhdWQiOiJUb2tlbnM6QXVkaWVuY2UifQ.yjE7zzCNUk8aMGkIkkmZk2IFEs-b8CvpT6r-OLLI2GA";
        private const string ErrorMessageInvalidToken = "Access token is invalid";
        private const string FacebookValidToken = "EAAMnrYZCCs68BAFLwsiOAXBi95piRosjnvU5BeZCkfxeTzS7lD5PCZAZBxXNogkGAkAw2N3arefWlXq7e6qb2HAKgyXj9Fh8y3szccJZBZAFPCYns6NYN2WjqkBnCLrWr3n7gbc07qspNZB8e61XPBhkUiFyVSFbF3i2W24yrwJ6gZDZD";
        private const string FacebookInvalidToken = "EAAMnrYZCCs68BAFLwsiOAXBi95piRosjnvU5BeZCkfxeTzS7lD5PCZAZBxXNogkGAkAw2N3arefWlXq7e6qb2HAKgyXj9Fh8y3szccJZBZAFPCYns6NYN2WjqkBnCLrWr3n7gbc07qspNZB8e6";
        private const string GoogleInvalidToken = "ya29.GlyMBnfx9gxRcYk99RO-LQx0AA2tbtHWL2kkkcQ_dn-XS9g1E89FX3sIlQ3Xs4PufhO9X2rR2k7stPlfZIGIyNhua7QicadcCTNAq-v9KTdjLG2i9zF7qtda6wX4Gg";
        private const string ErrorMessageLoginFailed = "Incorrect credentials entered";

        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            userManager = new Mock<IApplicationUserManager<User, int>>();
            jwtManager = new Mock<JWTManager>();
            subject = new LogInService(unitOfWork.Object, jwtManager.Object);
        }

        [Test]
        public void CreateToken()
        {
            LoginDTO model = new LoginDTO { Email = "sashatymoshchuk07@gmail.com", Password = "12345" };

            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            userManager.Setup(_ => _.CheckPasswordAsync(user, model.Password)).ReturnsAsync(model.Password == user.PasswordHash);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            jwtManager.Setup(_ => _.GenerateToken(user)).Returns(JwtTokenTest);

            var actual = subject.CreateToken(model);

            Assert.AreEqual(actual, JwtTokenTest);
        }

        [Test]
        public void CreateTokenWithFacebook_Throws_InvalidTokenException()
        {
            SocialAuthDTO token = new SocialAuthDTO { AccessToken = FacebookInvalidToken };

            userManager.Setup(_ => _.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            jwtManager.Setup(_ => _.GenerateToken(user)).Returns(JwtTokenTest);

            var ex = Assert.Throws<Exception>(() => subject.CreateTokenWithFacebook(token));

            Assert.That(ex.Message, Is.EqualTo(ErrorMessageInvalidToken));
        }

        [Test]
        public void CreateTokenWithGoogle_Throws_InvalidTokenException()
        {
            SocialAuthDTO token = new SocialAuthDTO { AccessToken = GoogleInvalidToken };

            userManager.Setup(_ => _.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);
            jwtManager.Setup(_ => _.GenerateToken(user)).Returns(JwtTokenTest);

            var ex = Assert.Throws<Exception>(() => subject.CreateTokenWithGoogle(token));

            Assert.That(ex.Message, Is.EqualTo(ErrorMessageInvalidToken));
        }

        [Test]
        public void LoginTest_UserNotFound_Throws_LoginFailedException()
        {
            LoginDTO model = new LoginDTO { Email = "", Password = "12345" };
            User user = null;

            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);

            var ex = Assert.Throws<LoginFailedException>(() => subject.CreateToken(model));

            Assert.That(ex.Message, Is.EqualTo(ErrorMessageLoginFailed));
        }

        [Test]
        public void LoginTest_IncorrectPassword_Throws_LoginFailedException()
        {
            LoginDTO model = new LoginDTO { Email = "", Password = "1234" };

            userManager.Setup(_ => _.FindByEmailAsync(model.Email)).ReturnsAsync(user);
            unitOfWork.Setup(_ => _.UserManager).Returns(userManager.Object);

            var ex = Assert.Throws<LoginFailedException>(() => subject.CreateToken(model));

            Assert.That(ex.Message, Is.EqualTo(ErrorMessageLoginFailed));
        }
    }
}
