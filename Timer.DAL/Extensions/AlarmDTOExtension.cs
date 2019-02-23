//-----------------------------------------------------------------------
// <copyright file="AlarmDtoExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;
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
                Id = 0,
                CronExpression = alarmDto.CronExpression,
                IsOn = alarmDto.IsOn,
                SoundOn = alarmDto.SoundOn,
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
                IsOn = alarm.IsOn,
                SoundOn = alarm.SoundOn,
                Message = alarm.Message,
                Timestamp = string.Join(",", alarm.Timestamp),
                UserId = alarm.UserId,
                IsUpdated = false
            };
        }

        /// <summary>
        /// Convert to alarmDto method
        /// </summary>
        /// <param name="alarmDto"> alarmDto model </param>
        /// <param name="alarm"> alarm model </param>
        public static void ToAlarm(this AlarmDto alarmDto, Alarm alarm)
        {
            alarm.CronExpression = alarmDto.CronExpression;
            alarm.IsOn = alarmDto.IsOn;
            alarm.SoundOn = alarmDto.SoundOn;
            alarm.Message = alarmDto.Message;
            alarm.Timestamp = alarmDto.Timestamp.Split(',').Select(n => Convert.ToByte(n)).ToArray();
            alarm.UserId = alarmDto.UserId;
        }

        /// <summary>
        /// Convert to alarmDto method
        /// </summary>
        /// <param name="alarm"> alarm model </param>
        /// <param name="alarmDto"> alarmDto model </param>
        public static void ToAlarmDto(this Alarm alarm, AlarmDto alarmDto)
        {
            alarmDto.Id = alarm.Id;
            alarmDto.CronExpression = alarm.CronExpression;
            alarmDto.IsOn = alarm.IsOn;
            alarmDto.SoundOn = alarm.SoundOn;
            alarmDto.Message = alarm.Message;
            if (alarm.Timestamp != null)
            {
                alarmDto.Timestamp = string.Join(",", alarm.Timestamp);
            }
            else
            {
                alarmDto.Timestamp = string.Empty;
            }

            alarmDto.IsUpdated = false;
            alarmDto.UserId = alarm.UserId;
        }
    }
}
