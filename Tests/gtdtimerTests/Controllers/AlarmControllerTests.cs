//-----------------------------------------------------------------------
// <copyright file="AlarmControllerTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using GtdCommon.ModelsDto;
using GtdTimer.Controllers;
using GtdServiceTier.Services;
using GtdTimerDAL.UnitOfWork;

namespace GtdTimerTests.Controllers
{
    [TestFixture]
    public class AlarmControllerTests : ControllerBase
    {
        private Mock<IAlarmService> alarmService;
        private Mock<IUserIdentityService> userIdentityService;
        private Mock<IUnitOfWork> unitOfWork;
        private AlarmController subject;
        private int userId = 215;
        private int alarmId = 9;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            alarmService = new Mock<IAlarmService>();
            userIdentityService = new Mock<IUserIdentityService>();
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new AlarmController(alarmService.Object, userIdentityService.Object);
        }

        /// <summary>
        /// Get all alarms by user id test
        /// </summary>
        [Test]
        public void GetAllAlarmsByUserId()
        {
            List<AlarmDto> alarms = new List<AlarmDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            alarmService.Setup(_ => _.GetAllAlarmsByUserId(userId)).Returns(alarms);
            var actual = (OkObjectResult)subject.GetAllAlarmsByUserId();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(alarms, actual.Value);
        }

        /// <summary>
        /// Create alarm test
        /// </summary>
        [Test]
        public void CreateAlarm()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            AlarmDto model = new AlarmDto();
            var actual = (OkObjectResult)subject.CreateAlarm(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            alarmService.Verify(_ => _.CreateAlarm(model), Times.Once);
        }

        /// <summary>
        /// Update alarm test
        /// </summary>
        [Test]
        public void UpdateAlarm()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            AlarmDto model = new AlarmDto();
            var actual = (OkObjectResult)subject.UpdateAlarm(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete alarm test
        /// </summary>
        [Test]
        public void DeleteAlarm()
        {
            var actual = (OkResult)subject.DeleteAlarm(alarmId);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            alarmService.Verify(_ => _.DeleteAlarmById(alarmId), Times.Once);
        }
    }
}
