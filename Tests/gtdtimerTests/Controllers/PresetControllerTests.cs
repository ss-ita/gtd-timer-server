using Common.Exceptions;
using Common.ModelsDTO;
using gtdtimer.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ServiceTier.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Timer.DAL.Extensions;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace gtdtimerTests.Controllers
{
    [TestFixture]
    public class PresetControllerTests
    {
        int timerid = 2;
        int presetID = 2;
        int userid = 315;
        TimerDTO timerDTO = new TimerDTO { Name = "timer", Id = 2 };
        PresetDTO presetDTO = new PresetDTO { PresetName = "string", UserId = 315, Id = 2 };
        private Mock<IPresetService> presetService;
        private Mock<ITimerService> timerService;
        private Mock<IUsersService> usersService;
        private Mock<IUserIdentityService> userIdentityService;
        private Mock<IUnitOfWork> unitOfWork;

        private PresetController subject;

        [SetUp]
        public void Setup()
        {
            presetService = new Mock<IPresetService>();
            timerService = new Mock<ITimerService>();
            usersService = new Mock<IUsersService>();
            userIdentityService = new Mock<IUserIdentityService>();
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new PresetController(userIdentityService.Object, presetService.Object,timerService.Object);
        }

        [Test]
        public void GetPresetById()
        {
            PresetDTO presetDTO = new PresetDTO();
            presetService.Setup(_ => _.GetPresetById(presetID)).Returns(presetDTO);

            var actual = (OkObjectResult)subject.GetPreset(presetID);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, presetDTO);
        }

        [Test]
        public void GetAllStandartPresets()
        {
            List<PresetDTO> presets = new List<PresetDTO>();
            presetService.Setup(_ => _.GetAllStandardPresets()).Returns(presets);

            var actual = (OkObjectResult)subject.GetAllStandardPresets();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, presets);
        }

        [Test]
        public void GetAllCustomPresets()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);
            List<PresetDTO> presets = new List<PresetDTO>();
            presetService.Setup(_ => _.GetAllCustomPresetsByUserId(userid)).Returns(presets);

            var actual = (OkObjectResult)subject.GetAllCustomPresets();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, presets);
        }

        [Test]
        public void Post()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);

            var actual = (OkResult)subject.CreatePreset(presetDTO);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.CreatePreset(presetDTO), Times.Once);
        }

        [Test]
        public void UpdatePreset()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);

            var actual = (OkResult)subject.UpdatePreset(presetDTO);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.UpdatePreset(presetDTO), Times.Once);
        }

        [Test]
        public void UpdateTimer()
        {
            var actual = (OkResult)subject.UpdateTimer(timerDTO);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            timerService.Verify(_ => _.UpdateTimer(timerDTO), Times.Once);
        }

        [Test]
        public void DeletePreset()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);
            presetService.Setup(_ => _.GetPresetById(presetID)).Returns(presetDTO);
            var actual = (OkResult)subject.DeletePreset(presetID);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.DeletePresetById(presetID), Times.Once);
        }

        [Test]
        public void DeleteTimer()
        {

            var actual = (OkResult)subject.DeleteTimer(timerid);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            timerService.Verify(_ => _.DeleteTimer(timerid), Times.Once);
        }
    }
}