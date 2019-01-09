using Common.ModelsDTO;
using System;
using MyTimer=Timer.DAL.Timer.DAL.Entities;

namespace Timer.DAL.Extensions
{
    public static class TimerDTOExtension
    {
        public static MyTimer.Timer ToTimer(this TimerDTO timerDTO)
        {
            return new MyTimer.Timer
            {
                Name = timerDTO.Name,
                Interval = TimeSpan.Parse(timerDTO.Interval),
                Id = timerDTO.Id,
                PresetId = timerDTO.PresetId
            };
        }

        public static TimerDTO ToTimerDTO(this MyTimer.Timer timer)
        {
            return new TimerDTO
            {
                Name = timer.Name,
                Interval = timer.Interval.ToString(),
                Id = timer.Id,
                PresetId = timer.Id
            };
        }
    }
}
