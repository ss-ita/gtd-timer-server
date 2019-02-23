//-----------------------------------------------------------------------
// <copyright file="AlarmServiceTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

using GtdCommon.ModelsDto;
using GtdServiceTier.Services;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;
using GtdTimerDAL.Extensions;

namespace GtdServiceTierTests
{
    public class AlarmServiceTests
    {
        private readonly int userId = 1;
        private Alarm alarm = new Alarm { Id = 1, CronExpression = "0 15 14 * * ? *", UserId = 1, Timestamp = new byte[] { 0, 0, 0, 0, 12, 14 } };
        private List<Alarm> alarms = new List<Alarm>();
        private Mock<IUnitOfWork> unitOfWork;
        private AlarmService subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new AlarmService(unitOfWork.Object);
            alarms.Add(alarm);
        }

        /// <summary>
        /// Create alarm test
        /// </summary>
        [Test]
        public void CreateAlarm()
        {
            AlarmDto alarm = new AlarmDto() { Timestamp = string.Empty };
            var alarmRepository = new Mock<IRepository<Alarm>>();
            
            unitOfWork.Setup(_ => _.Alarms).Returns(alarmRepository.Object);
          
            subject.CreateAlarm(alarm);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Update aask test
        /// </summary>
        [Test]
        public void UpdateAlarm()
        {
            AlarmDto alarmDto = new AlarmDto() { Id = 1, Timestamp = "0,0,0,0,12,14" };
            var alarmRepository = new Mock<IRepository<Alarm>>();

            unitOfWork.Setup(_ => _.Alarms).Returns(alarmRepository.Object);
            unitOfWork.Setup(_ => _.Alarms.GetByID(alarmDto.Id)).Returns(alarm);

            subject.UpdateAlarm(alarmDto);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Delete alarm test
        /// </summary>
        [Test]
        public void DeleteAlarm()
        {
            int alarmId = 2;
            Alarm alarm = new Alarm();
            var alarmRepository = new Mock<IRepository<Alarm>>();

            unitOfWork.Setup(_ => _.Alarms).Returns(alarmRepository.Object);
            unitOfWork.Setup(_ => _.Alarms.GetByID(alarmId)).Returns(alarm);

            subject.DeleteAlarmById(alarmId);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Get All alarms by user id test
        /// </summary>
        [Test]
        public void GetAllAlarmsByUserId()
        {
            var alarmRepository = new Mock<IRepository<Alarm>>();

            unitOfWork.Setup(_ => _.Alarms).Returns(alarmRepository.Object);
            unitOfWork.Setup(_ => _.Alarms.GetAllEntitiesByFilter(It.IsAny<Func<Alarm, bool>>())).Returns(alarms);

            Assert.AreEqual(subject.GetAllAlarmsByUserId(userId).ToList()[0].CronExpression, alarm.CronExpression);
        }
    }
}