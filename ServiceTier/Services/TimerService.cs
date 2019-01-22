//-----------------------------------------------------------------------
// <copyright file="TimerService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Extensions;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements i timer service
    /// </summary>
    public class TimerService : BaseService, ITimerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        public TimerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateTimer(TimerDto timerDto)
        {
            if (UnitOfWork.Presets.GetByID(timerDto.PresetId) == null)
            {
                throw new PresetNotFoundException();
            }

            UnitOfWork.Timers.Create(timerDto.ToTimer());
            UnitOfWork.Save();
        }

        public void UpdateTimer(TimerDto timerDto)
        {
            Timer timer = timerDto.ToTimer();
            UnitOfWork.Timers.Update(timer);
            UnitOfWork.Save();
        }

        public void DeleteTimer(int timerid)
        {
            UnitOfWork.Timers.Delete(timerid);
            UnitOfWork.Save();
        }

        public List<TimerDto> GetAllTimersByPresetId(int presetid)
        {
            var timerDtos = UnitOfWork.Timers.GetAllEntitiesByFilter(timer => timer.PresetId == presetid).Select(timer => timer.ToTimerDto()).ToList();        

            return timerDtos;
        }
    }
}
