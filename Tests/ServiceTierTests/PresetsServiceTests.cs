//-----------------------------------------------------------------------
// <copyright file="PresetsServiceTests.cs" company="SoftServe">
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
using GtdTimerDAL.Extensions;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTierTests
{
    public class PresetsServiceTests
    {
        private readonly int presetid = 1;
        private readonly int userid = 1;
        private readonly List<Timer> timers = new List<Timer>();
        private Preset preset = new Preset { Name = "preset", UserId = 1, Id = 1 };
        private List<Preset> presets = new List<Preset>();
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<ITimerService> timerService;      
        private PresetService subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            timerService = new Mock<ITimerService>();
            subject = new PresetService(unitOfWork.Object, timerService.Object);
            presets.Add(preset);
        }

        /// <summary>
        /// Create preset test
        /// </summary>
        [Test]
        public void CreatePreset()
        {
            List<TimerDto> timers = new List<TimerDto>();
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);

            subject.CreatePreset(preset.ToPresetDto(timers));

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Update Preset test
        /// </summary>
        [Test]
        public void UpdatePreset()
        {
            List<TimerDto> timers = new List<TimerDto>();
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);

            subject.UpdatePreset(preset.ToPresetDto(timers));

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Delete preset throws exception test
        /// </summary>
        [Test]
        public void ThrowsExceptionPresetDelete()
        {
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);

            var ex = Assert.Throws<PresetNotFoundException>(() => subject.DeletePresetById(presetid));

            Assert.That(ex.Message, Is.EqualTo("Preset does not Exist!"));
        }

        /// <summary>
        /// Get preset by id throws exception test
        /// </summary>
        [Test]
        public void ThrowsExceptionGetPresetById()
        {
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);

            var ex = Assert.Throws<PresetNotFoundException>(() => subject.GetPresetById(presetid));

            Assert.That(ex.Message, Is.EqualTo("Preset does not Exist!"));
        }

        /// <summary>
        /// Delete Preset test
        /// </summary>
        [Test]
        public void PresetDelete()
        {
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetByID(presetid)).Returns(preset);

            subject.DeletePresetById(presetid);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Get Preset By Id test
        /// </summary>
        [Test]
        public void GetPresetById()
        {
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetByID(presetid)).Returns(preset);

            Assert.AreEqual(subject.GetPresetById(presetid).PresetName, preset.Name);
        }

        /// <summary>
        /// Get All Standard Presets test
        /// </summary>
        [Test]
        public void GetAllStandartPresets()
        {
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetAllEntitiesByFilter(It.IsAny<Func<Preset, bool>>())).Returns(presets);
            unitOfWork.Setup(_ => _.Timers.GetAllEntities()).Returns(timers);
            preset.UserId = null;
            
            Assert.AreEqual(subject.GetAllStandardPresets()[0].PresetName, preset.Name);
        }

        /// <summary>
        /// Get All Custom Presets test
        /// </summary>
        [Test]
        public void GetAllCustomPresets()
        {
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetAllEntitiesByFilter(It.IsAny<Func<Preset, bool>>())).Returns(presets);
            unitOfWork.Setup(_ => _.Timers.GetAllEntities()).Returns(timers);

            Assert.AreEqual(subject.GetAllCustomPresetsByUserId(userid)[0].PresetName, preset.Name);
        }
    }
}
