
using Common.Exceptions;
using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timer.DAL.Extensions;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTier.Services
{
    public class TimerService:BaseService, ITimerService
    {
        public TimerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void CreateTimer(TimerDTO timerDTO)
        {
            if (unitOfWork.Presets.GetByID(timerDTO.PresetId) == null)
            {
                throw new PresetNotFoundException();
            }
            unitOfWork.Timers.Create(timerDTO.ToTimer());
            unitOfWork.Save();
        }

        public void UpdateTimer(TimerDTO timerDTO)
        {
            Timer.DAL.Timer.DAL.Entities.Timer timer = timerDTO.ToTimer();
            unitOfWork.Timers.Update(timer);
            unitOfWork.Save();
        }

        public void DeleteTimer(int timerid)
        {
            unitOfWork.Timers.Delete(timerid);
            unitOfWork.Save();
        }

        public List<TimerDTO> GetAllTimersByPresetId(int presetid)
        {
            var timerDTOs = unitOfWork.Timers.GetAllEntitiesByFilter(timer=>timer.PresetId==presetid).Select(timer => timer.ToTimerDTO()).ToList();           

            return timerDTOs;
        }
    }
}
