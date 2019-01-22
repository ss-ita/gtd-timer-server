//-----------------------------------------------------------------------
// <copyright file="PresetControllerTests.cs" company="SoftServe">
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
    public class PresetControllerTests
    {
        private readonly int timerid = 2;
        private readonly int presetID = 2;
        private readonly int userid = 315;
        private readonly TimerDto timerDto = new TimerDto { Name = "timer", Id = 2 };
        private readonly PresetDto presetDto = new PresetDto { PresetName = "string", UserId = 315, Id = 2 };
        private Mock<IPresetService> presetService;
        private Mock<ITimerService> timerService;
        private Mock<IUsersService> usersService;
        private Mock<IUserIdentityService> userIdentityService;
        private Mock<IUnitOfWork> unitOfWork;

        private PresetController subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            presetService = new Mock<IPresetService>();
            timerService = new Mock<ITimerService>();
            usersService = new Mock<IUsersService>();
            userIdentityService = new Mock<IUserIdentityService>();
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new PresetController(userIdentityService.Object, presetService.Object, timerService.Object);
        }

        /// <summary>
        /// Get Preset By Id test
        /// </summary>
        [Test]
        public void GetPresetById()
        {
            PresetDto presetDto = new PresetDto();
            presetService.Setup(_ => _.GetPresetById(presetID)).Returns(presetDto);

            var actual = (OkObjectResult)subject.GetPreset(presetID);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, presetDto);
        }

        /// <summary>
        /// Get All Standard Presets test
        /// </summary>
        [Test]
        public void GetAllStandardPresets()
        {
            List<PresetDto> presets = new List<PresetDto>();
            presetService.Setup(_ => _.GetAllStandardPresets()).Returns(presets);

            var actual = (OkObjectResult)subject.GetAllStandardPresets();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, presets);
        }

        /// <summary>
        /// Get All Custom Presets test
        /// </summary>
        [Test]
        public void GetAllCustomPresets()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);
            List<PresetDto> presets = new List<PresetDto>();
            presetService.Setup(_ => _.GetAllCustomPresetsByUserId(userid)).Returns(presets);

            var actual = (OkObjectResult)subject.GetAllCustomPresets();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, presets);
        }

        /// <summary>
        /// Create preset test
        /// </summary>
        [Test]
        public void Post()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);

            var actual = (OkResult)subject.CreatePreset(presetDto);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.CreatePreset(presetDto), Times.Once);
        }

        /// <summary>
        /// Update Preset test
        /// </summary>
        [Test]
        public void UpdatePreset()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);

            var actual = (OkResult)subject.UpdatePreset(presetDto);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.UpdatePreset(presetDto), Times.Once);
        }

        /// <summary>
        /// Update Timer test
        /// </summary>
        [Test]
        public void UpdateTimer()
        {
            var actual = (OkResult)subject.UpdateTimer(timerDto);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            timerService.Verify(_ => _.UpdateTimer(timerDto), Times.Once);
        }

        /// <summary>
        /// Delete Preset test
        /// </summary>
        [Test]
        public void DeletePreset()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);
            presetService.Setup(_ => _.GetPresetById(presetID)).Returns(presetDto);
            var actual = (OkResult)subject.DeletePreset(presetID);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.DeletePresetById(presetID), Times.Once);
        }

        /// <summary>
        /// Delete Timer test
        /// </summary>
        [Test]
        public void DeleteTimer()
        {
            var actual = (OkResult)subject.DeleteTimer(timerid);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            timerService.Verify(_ => _.DeleteTimer(timerid), Times.Once);
        }
    }
}