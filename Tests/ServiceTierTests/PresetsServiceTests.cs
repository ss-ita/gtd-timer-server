﻿using Common.Exceptions;
using Common.ModelsDTO;
using Moq;
using NUnit.Framework;
using ServiceTier.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timer.DAL.Extensions;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.Repositories;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTierTests
{
    class PresetsServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<ITimerService> timerService;

        private PresetService subject;

        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            timerService = new Mock<ITimerService>();
            subject = new PresetService(unitOfWork.Object,timerService.Object);
        }

        [Test]
        public void CreatePreset()
        {
            List<TimerDTO> timers = new List<TimerDTO>();
            Preset preset = new Preset { Name="preset", UserId=315 };
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);

            subject.CreatePreset(preset.ToPresetDTO(timers));

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void UpdatePreset()
        {
            List<TimerDTO> timers = new List<TimerDTO>();
            Preset preset = new Preset { Name = "preset", UserId = 315 };
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);

            subject.UpdatePreset(preset.ToPresetDTO(timers));

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void ThrowsExceptionPresetDelete()
        {
            int presetid = 1;
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);

            var ex = Assert.Throws<PresetNotFoundException>(() => subject.DeletePresetById(presetid));

            Assert.That(ex.Message, Is.EqualTo("Preset does not Exist!"));
        }

        [Test]
        public void ThrowsExceptionGetPresetById()
        {
            int presetid = 1;
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);

            var ex = Assert.Throws<PresetNotFoundException>(() => subject.GetPresetById(presetid));

            Assert.That(ex.Message, Is.EqualTo("Preset does not Exist!"));
        }

        [Test]
        public void PresetDelete()
        {
            int presetid = 1;
            Preset preset = new Preset { Name = "preset",UserId = 2, Id = 1 };
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetByID(presetid)).Returns(preset);

            subject.DeletePresetById(presetid);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void GetPresetById()
        {
            int presetid = 1;
            Preset preset = new Preset { Name = "preset", UserId = 2, Id = 1 };
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetByID(presetid)).Returns(preset);

            Assert.AreEqual(subject.GetPresetById(presetid).PresetName, preset.Name);
        }

        [Test]
        public void GetAllStandartPresets()
        {
            List<Preset> presets = new List<Preset>();
            List<Timer.DAL.Timer.DAL.Entities.Timer> timers = new List<Timer.DAL.Timer.DAL.Entities.Timer>();
            Preset preset = new Preset { Name = "preset", Id = 1 };
            presets.Add(preset);
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetAll()).Returns(presets.AsQueryable());
            unitOfWork.Setup(_ => _.Timers.GetAll()).Returns(timers.AsQueryable());

            Assert.AreEqual(subject.GetAllStandardPresets()[0].PresetName, preset.Name);
        }

        [Test]
        public void GetAllCustomPresets()
        {
            int userid = 1;
            List<Preset> presets = new List<Preset>();
            List<Timer.DAL.Timer.DAL.Entities.Timer> timers = new List<Timer.DAL.Timer.DAL.Entities.Timer>();
            Preset preset = new Preset { Name = "preset", Id = 1, UserId=1 };
            presets.Add(preset);
            var presetRepository = new Mock<IRepository<Preset>>();

            unitOfWork.Setup(_ => _.Presets).Returns(presetRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetAll()).Returns(presets.AsQueryable());
            unitOfWork.Setup(_ => _.Timers.GetAll()).Returns(timers.AsQueryable());

            Assert.AreEqual(subject.GetAllCustomPresetsByUserId(userid)[0].PresetName, preset.Name);
        }
    }
}