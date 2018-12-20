﻿using Common.Exceptions;
using gtdtimer.Controllers;
using gtdtimer.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServiceTier.Services;
using System.Net;
using Timer.DAL.Timer.DAL.Entities;

namespace gtdtimerTests.Controllers
{
    [TestFixture]
    public class SignUpControllerTests
    {
        private Mock<ISignUpService> signUpService;

        private SignUpController subject;

        [SetUp]
        public void Setup()
        {
            signUpService = new Mock<ISignUpService>();
            subject = new SignUpController(signUpService.Object);
        }

        [Test]
        public void GetByID()
        {
            int userID = 1;
            User user = new User();

            signUpService.Setup(_ => _.GetUserById(userID)).Returns(user);

            var actual = (OkObjectResult)subject.GetByID(userID);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, user);
        }

        [Test]
        public void GetByID_Throws_UserNotFoundException()
        {
            int userID = 1;

            signUpService.Setup(_ => _.GetUserById(userID)).Returns((User)null);

            var ex = Assert.Throws<UserNotFoundException>(() => subject.GetByID(userID));

            Assert.That(ex.Message, Is.EqualTo("User does not Exist!"));
        }

        [Test]
        public void Post()
        {
            UserDTO model = new UserDTO();

            var actual = (OkResult)subject.Post(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            signUpService.Verify(_ => _.AddUser(model), Times.Once);
        }

        [Test]
        public void Post_Throws_UserAlreadyExistsException()
        {
            UserDTO model = new UserDTO();

            signUpService.Setup(_ => _.AddUser(model)).Throws(new UserAlreadyExistsException());

            var ex = Assert.Throws<UserAlreadyExistsException>(() => subject.Post(model));

            Assert.That(ex.Message, Is.EqualTo("User with such email address already exists"));
        }
    }
}