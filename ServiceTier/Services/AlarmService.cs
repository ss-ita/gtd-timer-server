//-----------------------------------------------------------------------
// <copyright file="AlarmService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdTimerDAL.Extensions;
using GtdTimerDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using DefaultXmlSerializer = System.Xml.Serialization.XmlSerializer;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements i unit of work interface
    /// </summary>
    public class AlarmService : BaseService, IAlarmService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        public AlarmService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateAlarm(AlarmDto alarmDto)
        {
            var alarm = alarmDto.ToAlarm();
            UnitOfWork.Alarms.Create(alarm);
            UnitOfWork.Save();
            alarm.ToAlarmDto(alarmDto);
          /*  alarmDto.Id = alarm.Id;
            if (alarm.Timestamp != null) {
                alarmDto.Timestamp = string.Join(",", alarm.Timestamp);
            } else
            {
                alarmDto.Timestamp = "";
            }
            alarmDto.IsUpdated = false;*/
        }

        public void DeleteAlarmById(int alarmId)
        {
            var toDelete = UnitOfWork.Alarms.GetByID(alarmId);
            if (toDelete != null)
            {
                UnitOfWork.Alarms.Delete(toDelete);
                UnitOfWork.Save();
            }
            else
            {
                throw new Exception("Alarm does not exist!");
            }
        }

        public IEnumerable<AlarmDto> GetAllAlarmsByUserId(int userId)
        {
            var listOfAlarmsDto = UnitOfWork.Alarms.GetAllEntitiesByFilter(user => user.UserId == userId)
                .Select(alarm => alarm.ToAlarmDto())
                .ToList();

            return listOfAlarmsDto;
        }

        public void UpdateAlarm(AlarmDto alarmDto)
        {
            var alarm = UnitOfWork.Alarms.GetByID(alarmDto.Id);

            var alarmTimestamp = string.Join(",", alarm.Timestamp);
            if (alarmTimestamp == alarmDto.Timestamp)
            {
                alarmDto.ToAlarm(alarm);
                UnitOfWork.Alarms.Update(alarm);
                UnitOfWork.Save();
                alarmDto.Timestamp = string.Join(",", alarm.Timestamp);
                alarmDto.IsUpdated = true;
            }
            else
            {
                alarm.ToAlarmDto(alarmDto);
                alarmDto.IsUpdated = false;
            }
        }
    }
}
