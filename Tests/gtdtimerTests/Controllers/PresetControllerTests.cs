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
            int presetID = 2;
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
            int userid = 315;
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
            int userid = 315;
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);
            PresetDTO presetDTO = new PresetDTO { PresetName = "string" };

            var actual = (OkResult)subject.Post(presetDTO);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.CreatePreset(presetDTO), Times.Once);
        }

        [Test]
        public void UpdatePreset()
        {
            int userid = 315;
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userid);
            PresetDTO presetDTO = new PresetDTO { PresetName = "string", UserId = 315, Id=2 };

            var actual = (OkResult)subject.UpdatePreset(presetDTO);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.UpdatePreset(presetDTO), Times.Once);
        }

        [Test]
        public void UpdateTimer()
        {
            TimerDTO timerDTO = new TimerDTO { Name="timer", Id = 2 };

            var actual = (OkResult)subject.UpdateTimer(timerDTO);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            timerService.Verify(_ => _.UpdateTimer(timerDTO), Times.Once);
        }

        [Test]
        public void DeletePreset()
        {
            int presetid = 8;

            var actual = (OkResult)subject.DeletePreset(presetid);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            presetService.Verify(_ => _.DeletePresetById(presetid), Times.Once);
        }

        [Test]
        public void DeleteTimer()
        {
            int timerid = 2;

            var actual = (OkResult)subject.DeleteTimer(timerid);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            timerService.Verify(_ => _.DeleteTimer(timerid), Times.Once);
        }
    }
}