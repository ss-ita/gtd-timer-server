using Common.ModelsDTO;
using System;

namespace Timer.DAL.Extensions
{
    public static class TimerDTOExtension
    {
        public static Timer.DAL.Entities.Timer ToTimer(this TimerDTO timerDTO)
        {
            return new Timer.DAL.Entities.Timer
            {
                Name = timerDTO.Name,
                Interval = TimeSpan.Parse(timerDTO.Interval),
                Id = timerDTO.Id,
                PresetId = timerDTO.PresetId
            };
        }

        public static TimerDTO ToTimerDTO(this Timer.DAL.Entities.Timer timer)
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
