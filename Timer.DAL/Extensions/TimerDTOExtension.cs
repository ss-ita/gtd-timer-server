//-----------------------------------------------------------------------
// <copyright file="TimerDtoExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;

using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Extensions
{
    /// <summary>
    /// TimerDtoExtension class for converting to timer and vice versa
    /// </summary>
    public static class TimerDtoExtension
    {
        /// <summary>
        /// Convert to timer method
        /// </summary>
        /// <param name="timerDto"> timerDto model </param>
        /// <returns>returns timer</returns>
        public static Timer ToTimer(this TimerDto timerDto)
        {
            return new Timer
            {
                Name = timerDto.Name,
                Interval = TimeSpan.Parse(timerDto.Interval),
                Id = timerDto.Id,
                PresetId = timerDto.PresetId
            };
        }

        /// <summary>
        /// Convert to timerDto method
        /// </summary>
        /// <param name="timer"> timer model </param>
        /// <returns>returns timerDto</returns>
        public static TimerDto ToTimerDto(this Timer timer)
        {
            return new TimerDto
            {
                Name = timer.Name,
                Interval = timer.Interval.ToString(),
                Id = timer.Id,
                PresetId = timer.Id
            };
        }
    }
}
