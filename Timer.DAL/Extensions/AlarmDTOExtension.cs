//-----------------------------------------------------------------------
// <copyright file="AlarmDtoExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;

using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Extensions
{
    /// <summary>
    /// AlarmDtoExtension class for converting to alarm and vice versa
    /// </summary>
    public static class AlarmDtoExtension
    {
        /// <summary>
        /// Convert to alarm method
        /// </summary>
        /// <param name="alarmDto"> alarmDto model </param>
        /// <returns>returns alarms</returns>
        public static Alarm ToAlarm(this AlarmDto alarmDto)
        {
            return new Alarm
            {
                Id = alarmDto.Id,
                CronExpression = alarmDto.CronExpression,
                IsTurnOn = alarmDto.IsTurnOn,
                IsSound = alarmDto.IsSound,
                Message = alarmDto.Message,
                UserId = alarmDto.UserId
            };
        }

        /// <summary>
        /// Convert to alarmDto method
        /// </summary>
        /// <param name="alarm"> alarm model </param>
        /// <returns>returns alarmDto</returns>
        public static AlarmDto ToAlarmDto(this Alarm alarm)
        {
            return new AlarmDto
            {
                Id = alarm.Id,
                CronExpression = alarm.CronExpression,
                IsTurnOn = alarm.IsTurnOn,
                IsSound = alarm.IsSound,
                Message = alarm.Message,
                UserId = alarm.UserId
            };
        }
    }
}
