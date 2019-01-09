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
        private Mock<IUserIdentityService> userIdentityService;
        private Mock<IUnitOfWork> unitOfWork;

        private PresetController subject;

        [SetUp]
        public void Setup()
        {
            presetService = new Mock<IPresetService>();
            timerService = new Mock<ITimerService>();
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
    }
}