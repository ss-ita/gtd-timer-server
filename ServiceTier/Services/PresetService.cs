//-----------------------------------------------------------------------
// <copyright file="PresetService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdTimerDAL.Extensions;
using GtdTimerDAL.Entities;
using GtdTimerDAL.UnitOfWork;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements i preset service interface
    /// </summary>
    public class PresetService : BaseService, IPresetService
    {
        /// <summary>
        /// instance of timer service
        /// </summary>
        private readonly ITimerService timerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresetService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        /// <param name="timerService">instance of timer service</param>
        public PresetService(IUnitOfWork unitOfWork, ITimerService timerService) : base(unitOfWork)
        {
            this.timerService = timerService;
        }

        public void CreatePreset(PresetDto presetDto)
        {
            Preset preset = presetDto.ToPreset();
            UnitOfWork.Presets.Create(preset);
            UnitOfWork.Save();
            presetDto.Id = preset.Id;

            foreach (var timer in presetDto.Timers)
            {
                timer.PresetId = preset.Id;
                this.timerService.CreateTimer(timer);
            }
        }

        public void UpdatePreset(PresetDto presetDto)
        {
            Preset preset = presetDto.ToPreset();
            foreach (var timer in presetDto.Timers)
            {
                timerService.UpdateTimer(timer);
            }
            UnitOfWork.Presets.Update(preset);
            UnitOfWork.Save();
        }

        public void DeletePresetById(int presetid)
        {
            if (UnitOfWork.Presets.GetByID(presetid) == null)
            {
                throw new PresetNotFoundException();
            }

            UnitOfWork.Presets.Delete(presetid);
            UnitOfWork.Save();
        }

        public PresetDto GetPresetById(int presetid)
        {
            if (UnitOfWork.Presets.GetByID(presetid) == null)
            {
                throw new PresetNotFoundException();
            }

            var preset = UnitOfWork.Presets.GetByID(presetid);
            return preset.ToPresetDto(this.timerService.GetAllTimersByPresetId(presetid));
        }

        public IList<PresetDto> GetAllCustomPresetsByUserId(int userid)
        {
            var listOfPresetsDto = new List<PresetDto>();
            var presets = UnitOfWork.Presets.GetAllEntitiesByFilter(preset => preset.UserId == userid);
            var timers = UnitOfWork.Timers.GetAllEntities();

            foreach (var preset in presets)
            {
                    List<TimerDto> timerDtos = timerService.GetAllTimersByPresetId(preset.Id);
                    listOfPresetsDto.Add(preset.ToPresetDto(timerDtos));
            }

            return listOfPresetsDto;
        }

        public IList<PresetDto> GetAllStandardPresets()
        {
            var listOfPresetsDto = new List<PresetDto>();
            var presets = UnitOfWork.Presets.GetAllEntitiesByFilter(preset => preset.UserId == null);
            var timers = UnitOfWork.Timers.GetAllEntities();

            foreach (var preset in presets)
            {
                var timerDtos = timerService.GetAllTimersByPresetId(preset.Id);
                    listOfPresetsDto.Add(preset.ToPresetDto(timerDtos));
            }

            return listOfPresetsDto;
        }
    }
}
