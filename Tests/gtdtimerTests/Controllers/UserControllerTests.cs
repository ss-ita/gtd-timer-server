//-----------------------------------------------------------------------
// <copyright file="UserControllerTests.cs" company="SoftServe">
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
using GtdTimerDAL.Entities;

namespace GtdTimerTests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private const string userId = "0";
        private const string token = "User Token";
        private Mock<IUsersService> usersService;
        private Mock<IUserIdentityService> userIdentityService;

        private UserController subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            usersService = new Mock<IUsersService>();
            userIdentityService = new Mock<IUserIdentityService>();
            subject = new UserController(userIdentityService.Object, usersService.Object);
        }

        /// <summary>
        /// get user by id test
        /// </summary>
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

        /// <summary>
        /// User Not Found Exception test
        /// </summary>
        [Test]
        public void Get_Throws_UserNotFoundException()
        {
            int userID = 1;

            userIdentityService.Setup(_ => _.GetUserId()).Returns(userID);
            usersService.Setup(_ => _.Get(userID)).Returns((User)null);

            var ex = Assert.Throws<UserNotFoundException>(() => subject.Get());

            Assert.That(ex.Message, Is.EqualTo("User does not Exist!"));
        }

        /// <summary>
        /// Create user test
        /// </summary>
        [Test]
        public void Post()
        {
            UserDto model = new UserDto();

            var actual = (OkResult)subject.Post(model);

            usersService.Verify(_ => _.Create(model), Times.Once);
            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Verify token test
        /// </summary>
        [Test]
        public void VerifyToken()
        {
            var actual = (OkResult)subject.VerifyEmail(userId, token);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// User Already Exists Exception test
        /// </summary>
        [Test]
        public void Post_Throws_UserAlreadyExistsException()
        {
            UserDto model = new UserDto();

            usersService.Setup(_ => _.Create(model)).Throws(new UserAlreadyExistsException());

            var ex = Assert.Throws<UserAlreadyExistsException>(() => subject.Post(model));

            Assert.That(ex.Message, Is.EqualTo("User with such email address already exists"));
        }

        /// <summary>
        /// update password test
        /// </summary>
        [Test]
        public void Put()
        {
            int userID = 1;
            User user = new User();
            UpdatePasswordDto model = new UpdatePasswordDto();

            userIdentityService.Setup(_ => _.GetUserId()).Returns(userID);
            usersService.Setup(_ => _.Get(userID)).Returns(user);

            var actual = (OkResult)subject.Put(model);

            usersService.Verify(_ => _.UpdatePassword(userID, model), Times.Once);
            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// delete user test
        /// </summary>
        [Test]
        public void Delete()
        {
            int userID = 1;

            userIdentityService.Setup(_ => _.GetUserId()).Returns(userID);

            var actual = (OkResult)subject.Delete();

            usersService.Verify(_ => _.Delete(userID), Times.Once);
            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Add Role Test
        /// </summary>
        [Test]
        public void AddRoleTest_ReturnsOkRequest_WhenModelCorect()
        {
            RoleDto model = new RoleDto() { Email = Constants.CorectEmail, Role = Constants.AdminRole };

            var actual = (OkResult)subject.AdDtoRole(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Remove Role Test
        /// </summary>
        [Test]
        public void RemoveRoleTest_ReturnsOkRequest_WhenModelCorect()
        {
            RoleDto model = new RoleDto() { Email = Constants.CorectEmail, Role = Constants.AdminRole };

            var actual = (OkResult)subject.RemoveFromRoles(model.Email, model.Role);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Get All Emails Test
        /// </summary>
        [Test]
        public void GetAllEmailTest_ReturnsOkRequest()
        {
            var actual = (OkObjectResult)subject.GetUsersEmails(Constants.AdminRole);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Add Role Throws User Not Found Exception test
        /// </summary>
        [Test]
        public void GetRolesOfUserTest_ReturnsOkRequest()
        {
            var actual = (OkObjectResult)subject.GetRolesOfUser();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Test checks if user is deleted corectly
        /// </summary>
        [Test]
        public void DeleteUserByEmailTest_ReturnsOkRequest_WhenEmailCorect()
        {
            string email = Constants.CorectEmail;

            var actual = (OkResult)subject.DeleteUserByEmail(email);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }
    }
}