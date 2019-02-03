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
using System.Linq;

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
        private readonly ITaskService taskService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresetService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        /// <param name="timerService">instance of timer service</param>
        public PresetService(IUnitOfWork unitOfWork, ITaskService taskService) : base(unitOfWork)
        {
            this.taskService = taskService;
        }

        public void CreatePreset(PresetDto presetDto)
        {
            Preset preset = presetDto.ToPreset();
            UnitOfWork.Presets.Create(preset);
            UnitOfWork.Save();
            presetDto.Id = preset.Id;

            foreach (var task in presetDto.Tasks)
            {
                taskService.CreateTask(task);
                PresetTasks presettask = new PresetTasks
                {
                    PresetId = preset.Id,
                    TaskId = task.Id
                };
                UnitOfWork.PresetTasks.Create(presettask);
                UnitOfWork.Save();
            }
        }

        public void UpdatePreset(PresetDto presetDto)
        {
            Preset preset = presetDto.ToPreset();
            foreach (var task in presetDto.Tasks)
            {
                if (task.Id == 0)
                {
                    taskService.CreateTask(task);
                    UnitOfWork.PresetTasks.Create(new PresetTasks
                    {
                        PresetId = preset.Id,
                        TaskId = task.Id
                    });
                    UnitOfWork.Save();
                }
                else
                {
                    taskService.UpdateTask(task);
                }
            }
            UnitOfWork.Presets.Update(preset);
            UnitOfWork.Save();
            presetDto.Id = preset.Id;
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

            var taskDtos = UnitOfWork.PresetTasks.GetAllEntitiesByFilter(presettask => presettask.PresetId == presetid)
                .Select(presettask => UnitOfWork.Tasks.GetByID(presettask.TaskId).ToTaskDto()).ToList();

            return UnitOfWork.Presets.GetByID(presetid).ToPresetDto(taskDtos);
        }

        public IList<PresetDto> GetAllCustomPresetsByUserId(int userid)
        {
            var listOfPresetsDto = new List<PresetDto>();
            var presets = UnitOfWork.Presets.GetAllEntitiesByFilter(preset => preset.UserId == userid);
            var timers = UnitOfWork.Tasks.GetAllEntities();

            foreach (var preset in presets)
            {
                var taskDtos = UnitOfWork.PresetTasks.GetAllEntitiesByFilter(presettask => presettask.PresetId == preset.Id)
                    .Select(presettask => UnitOfWork.Tasks.GetByID(presettask.TaskId).ToTaskDto()).ToList();
                listOfPresetsDto.Add(preset.ToPresetDto(taskDtos));
            }

            return listOfPresetsDto;
        }

        public IList<PresetDto> GetAllStandardPresets()
        {
            var listOfPresetsDto = new List<PresetDto>();
            var presets = UnitOfWork.Presets.GetAllEntitiesByFilter(preset => preset.UserId == null);
            foreach (var preset in presets)
            {
                var taskDtos = UnitOfWork.PresetTasks.GetAllEntitiesByFilter(presettask => presettask.PresetId == preset.Id)
                   .Select(presettask => UnitOfWork.Tasks.GetByID(presettask.TaskId).ToTaskDto()).ToList();
                listOfPresetsDto.Add(preset.ToPresetDto(taskDtos));
            }

            return listOfPresetsDto;
        }
    }
}
