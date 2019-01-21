using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

using Common.Constant;
using Common.Exceptions;
using Common.ModelsDTO;
using gtdtimer.Controllers;
using Moq;
using NUnit.Framework;
using ServiceTier.Services;
using Timer.DAL.Timer.DAL.Entities;

namespace gtdtimerTests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUsersService> usersService;
        private Mock<IUserIdentityService> userIdentityService;

        private UserController subject;

        [SetUp]
        public void Setup()
        {
            usersService = new Mock<IUsersService>();
            userIdentityService = new Mock<IUserIdentityService>();
            subject = new UserController(userIdentityService.Object, usersService.Object);
        }

        [Test]
        public void Get()
        {
            int userID = 1;
            User user = new User();

            userIdentityService.Setup(_ => _.GetUserId()).Returns(userID);
            usersService.Setup(_ => _.Get(userID)).Returns(user);

            var actual = (OkObjectResult)subject.Get();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, user);
        }

        [Test]
        public void Get_Throws_UserNotFoundException()
        {
            int userID = 1;

            userIdentityService.Setup(_ => _.GetUserId()).Returns(userID);
            usersService.Setup(_ => _.Get(userID)).Returns((User)null);

            var ex = Assert.Throws<UserNotFoundException>(() => subject.Get());

            Assert.That(ex.Message, Is.EqualTo("User does not Exist!"));
        }

        [Test]
        public void Post()
        {
            UserDTO model = new UserDTO();

            var actual = (OkResult)subject.Post(model);

            usersService.Verify(_ => _.Create(model), Times.Once);
            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void Post_Throws_UserAlreadyExistsException()
        {
            UserDTO model = new UserDTO();

            usersService.Setup(_ => _.Create(model)).Throws(new UserAlreadyExistsException());

            var ex = Assert.Throws<UserAlreadyExistsException>(() => subject.Post(model));

            Assert.That(ex.Message, Is.EqualTo("User with such email address already exists"));
        }

        [Test]
        public void Put()
        {
            int userID = 1;
            User user = new User();
            UpdatePasswordDTO model = new UpdatePasswordDTO();

            userIdentityService.Setup(_ => _.GetUserId()).Returns(userID);
            usersService.Setup(_ => _.Get(userID)).Returns(user);

            var actual = (OkResult)subject.Put(model);

            usersService.Verify(_ => _.UpdatePassword(userID, model), Times.Once);
            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void Delete()
        {
            int userID = 1;

            userIdentityService.Setup(_ => _.GetUserId()).Returns(userID);

            var actual = (OkResult)subject.Delete();

            usersService.Verify(_ => _.Delete(userID), Times.Once);
            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void AddRoleTest_ReturnsOkRequest_WhenModelCorect()
        {
            RoleDTO model = new RoleDTO() { Email = Constants.CorectEmail, Role = Constants.AdminRole };

            var actual = (OkResult)subject.AddToRole(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void RemoveRoleTest_ReturnsOkRequest_WhenModelCorect()
        {
            RoleDTO model = new RoleDTO() { Email = Constants.CorectEmail, Role = Constants.AdminRole };

            var actual = (OkResult)subject.RemoveFromRoles(model.Email, model.Role);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void GetAllEmailTest_ReturnsOkRequest()
        {
            var actual = (OkObjectResult) subject.GetUsersEmails();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        [Test]
        public void GetRolesOfUserTest_ReturnsOkRequest()
        {
            var actual = (OkObjectResult)subject.GetRolesOfUser();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        public void DeleteUserByEmailTest_ReturnsOkRequest_WhenEmailCorect()
        {
            string email = Constants.CorectEmail;

            var actual = (OkResult)subject.DeleteUserByEmail(email);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }
    }
}