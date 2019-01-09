﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Exceptions;
using Common.ModelsDTO;
using Timer.DAL.Extensions;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTier.Services
{
    public class PresetService : BaseService, IPresetService
    {
        private readonly ITimerService timerService;

        public PresetService(IUnitOfWork unitOfWork, ITimerService timerService) : base(unitOfWork)
        {
            this.timerService = timerService;
        }
        public void CreatePreset(PresetDTO presetDTO)
        {
            Preset preset = presetDTO.ToPreset();
            unitOfWork.Presets.Create(preset);
            unitOfWork.Save();
            foreach (var timer in presetDTO.Timers)
            {
                timer.PresetId = preset.Id;
                timerService.CreateTimer(timer);
            }
        }

        public void UpdatePreset(PresetDTO presetDTO)
        {
            Preset preset = presetDTO.ToPreset();
            unitOfWork.Presets.Update(preset);
            unitOfWork.Save();
        }

        public void DeletePresetById(int presetid)
        {
            if (unitOfWork.Presets.GetByID(presetid) == null)
            {
                throw new PresetNotFoundException();
            }
            if (unitOfWork.Presets.GetByID(presetid).UserId == null)
            {
                throw new StandartPresetException();
            }
            unitOfWork.Presets.Delete(presetid);
            unitOfWork.Save();
        }

        public PresetDTO GetPresetById(int presetid)
        {
            if (unitOfWork.Presets.GetByID(presetid) == null)
            {
                throw new PresetNotFoundException();
            }
            var preset = unitOfWork.Presets.GetByID(presetid);

            return preset.ToPresetDTO(timerService.GetAllTimersByPresetId(presetid));
        }

        public List<PresetDTO> GetAllCustomPresetsByUserId(int userid)
        {
            var listOfPresetsDTO = new List<PresetDTO>();
            var presets = unitOfWork.Presets.GetAll();
            var timers = unitOfWork.Timers.GetAll();

            foreach (var preset in presets)
            {
                if (preset.UserId == userid)
                {
                    List<TimerDTO> timerDTOs = timerService.GetAllTimersByPresetId(preset.Id);
                    listOfPresetsDTO.Add(preset.ToPresetDTO(timerDTOs));
                }
            }

            return listOfPresetsDTO;
        }

        public List<PresetDTO> GetAllStandardPresets()
        {
            var listOfPresetsDTO = new List<PresetDTO>();
            var presets = unitOfWork.Presets.GetAll();
            var timers = unitOfWork.Timers.GetAll();

            foreach (var preset in presets)
            {
                if (preset.UserId == null)
                {
                    var timerDTOs= timerService.GetAllTimersByPresetId(preset.Id);
                    listOfPresetsDTO.Add(preset.ToPresetDTO(timerDTOs));
                }
            }

            return listOfPresetsDTO;
        }

    }
}