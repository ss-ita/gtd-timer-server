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
    class TimerServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;

        private TimerService subject;

        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new TimerService(unitOfWork.Object);
        }

        [Test]
        public void CreateTimer()
        {
            TimerDTO timer = new TimerDTO { Name = "timer", PresetId = 2, Interval = "0:5:0", Id = 1 };
            Preset preset = new Preset { Name = "preset", Id = 1, UserId = 315 };
            var timerRepository = new Mock<IRepository<Timer.DAL.Timer.DAL.Entities.Timer>>();

            unitOfWork.Setup(_ => _.Timers).Returns(timerRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetByID(timer.PresetId)).Returns(preset);

            subject.CreateTimer(timer);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void UpdateTimer()
        {
            TimerDTO timer = new TimerDTO { Name = "timer", PresetId = 2, Interval = "0:5:0", Id = 1 };
            Preset preset = new Preset { Name = "preset", Id = 1, UserId = 315 };
            var timerRepository = new Mock<IRepository<Timer.DAL.Timer.DAL.Entities.Timer>>();

            unitOfWork.Setup(_ => _.Timers).Returns(timerRepository.Object);
            unitOfWork.Setup(_ => _.Presets.GetByID(timer.PresetId)).Returns(preset);

            subject.UpdateTimer(timer);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void TimerDelete()
        {
            int timerid = 1;
            Timer.DAL.Timer.DAL.Entities.Timer timer = new Timer.DAL.Timer.DAL.Entities.Timer { Name = "timer", PresetId = 2, Interval = TimeSpan.Parse("0:5:0"), Id = 1 };
            var timerRepository = new Mock<IRepository<Timer.DAL.Timer.DAL.Entities.Timer>>();

            unitOfWork.Setup(_ => _.Timers).Returns(timerRepository.Object);
            unitOfWork.Setup(_ => _.Timers.GetByID(timerid)).Returns(timer);

            subject.DeleteTimer(timerid);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        [Test]
        public void GetAllTimersByPresetId()
        {
            int presetid = 12;
            List<Timer.DAL.Timer.DAL.Entities.Timer> timers = new List<Timer.DAL.Timer.DAL.Entities.Timer>();
            List<TimerDTO> timerDTOs = new List<TimerDTO>();
            Timer.DAL.Timer.DAL.Entities.Timer timer = new Timer.DAL.Timer.DAL.Entities.Timer { Name = "timer", PresetId = 12, Interval = TimeSpan.Parse("0:5:0"), Id = 1 };
            timers.Add(timer);
            timerDTOs.Add(timer.ToTimerDTO());
            var timerRepository = new Mock<IRepository<Timer.DAL.Timer.DAL.Entities.Timer>>();

            unitOfWork.Setup(_ => _.Timers).Returns(timerRepository.Object);
            unitOfWork.Setup(_ => _.Timers.GetAllEntitiesByFilter(It.IsAny<Func<Timer.DAL.Timer.DAL.Entities.Timer, bool>>())).Returns(timers);

            Assert.AreEqual(subject.GetAllTimersByPresetId(presetid)[0].Interval,timerDTOs[0].Interval);
        }
    }
}
