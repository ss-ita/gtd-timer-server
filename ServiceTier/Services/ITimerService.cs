using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceTier.Services
{
    public interface ITimerService : IBaseService
    {
        void CreateTimer(TimerDTO timerDTO);
        void UpdateTimer(TimerDTO timerDTO);
        void DeleteTimer(int timerid);
        List<TimerDTO> GetAllTimersByPresetId(int presetid);
    }
}
