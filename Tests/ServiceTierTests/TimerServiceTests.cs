//-----------------------------------------------------------------------
// <copyright file="TimerServiceTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

using GtdCommon.ModelsDto;
using GtdServiceTier.Services;
using GtdTimerDAL.Extensions;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTierTests
{
    public class TimerServiceTests
    {
        private Mock<IUnitOfWork> unitOfWork;

        private TimerService subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new TimerService(unitOfWork.Object);
        }

        /// <summary>
        /// Create timer test
        /// </summary>
        //[Test]
        //public void CreateTimer()
        //{
        //    TimerDto timer = new TimerDto { Name = "timer", PresetId = 2, Interval = "0:5:0", Id = 1 };
        //    Preset preset = new Preset { Name = "preset", Id = 1, UserId = 315 };
        //    var timerRepository = new Mock<IRepository<Timer>>();

        //    unitOfWork.Setup(_ => _.Timers).Returns(timerRepository.Object);
        //    unitOfWork.Setup(_ => _.Presets.GetByID(timer.PresetId)).Returns(preset);

        //    subject.CreateTimer(timer);

        //    unitOfWork.Verify(_ => _.Save(), Times.Once);
        //}

        /// <summary>
        /// Update Timer test
        /// </summary>
        //[Test]
        //public void UpdateTimer()
        //{
        //    TimerDto timer = new TimerDto { Name = "timer", PresetId = 2, Interval = "0:5:0", Id = 1 };
        //    Preset preset = new Preset { Name = "preset", Id = 1, UserId = 315 };
        //    var timerRepository = new Mock<IRepository<Timer>>();

        //    unitOfWork.Setup(_ => _.Timers).Returns(timerRepository.Object);
        //    unitOfWork.Setup(_ => _.Presets.GetByID(timer.PresetId)).Returns(preset);

        //    subject.UpdateTimer(timer);

        //    unitOfWork.Verify(_ => _.Save(), Times.Once);
        //}

        /// <summary>
        /// Delete Timer test
        /// </summary>
        //[Test]
        //public void TimerDelete()
        //{
        //    int timerid = 1;
        //    Timer timer = new Timer { Name = "timer", PresetId = 2, Interval = TimeSpan.Parse("0:5:0"), Id = 1 };
        //    var timerRepository = new Mock<IRepository<Timer>>();

        //    unitOfWork.Setup(_ => _.Timers).Returns(timerRepository.Object);
        //    unitOfWork.Setup(_ => _.Timers.GetByID(timerid)).Returns(timer);

        //    subject.DeleteTimer(timerid);

        //    unitOfWork.Verify(_ => _.Save(), Times.Once);
        //}

        /// <summary>
        /// Get All Timers By Preset Id test
        /// </summary>
        //[Test]
        //public void GetAllTimersByPresetId()
        //{
        //    int presetid = 12;
        //    List<Timer> timers = new List<Timer>();
        //    List<TimerDto> timerDtos = new List<TimerDto>();
        //    Timer timer = new Timer { Name = "timer", PresetId = 12, Interval = TimeSpan.Parse("0:5:0"), Id = 1 };
        //    timers.Add(timer);
        //    timerDtos.Add(timer.ToTimerDto());
        //    var timerRepository = new Mock<IRepository<Timer>>();

        //    unitOfWork.Setup(_ => _.Timers).Returns(timerRepository.Object);
        //    unitOfWork.Setup(_ => _.Timers.GetAllEntitiesByFilter(It.IsAny<Func<Timer, bool>>())).Returns(timers);

        //    Assert.AreEqual(subject.GetAllTimersByPresetId(presetid)[0].Interval, timerDtos[0].Interval);
        //}
    }
}
