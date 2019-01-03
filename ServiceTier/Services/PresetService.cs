using System;
using System.Collections.Generic;
using System.Linq;
using Common.ModelsDTO;
using Timer.DAL.Extensions;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTier.Services
{
    public class PresetService : BaseService, IPresetService
    {
        public PresetService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void CreatePreset(PresetDTO presetDTO)
        {
            Preset preset = presetDTO.ToPreset();
            unitOfWork.Presets.Create(preset);
            unitOfWork.Save();
            var presets = unitOfWork.Presets.GetAll();
            foreach (var timer in presetDTO.Timers)
            {
                timer.PresetId = preset.Id;
                unitOfWork.Timers.Create(timer.ToTimer());
                unitOfWork.Save();
            }
        }

        public void UpdatePreset(PresetDTO presetDTO)
        {
            Preset preset = presetDTO.ToPreset();
            unitOfWork.Presets.Update(preset);
            unitOfWork.Save();
        }

        public void UpdateTimer(TimerDTO timerDTO)
        {
            Timer.DAL.Timer.DAL.Entities.Timer timer = timerDTO.ToTimer();
            unitOfWork.Timers.Update(timer);
            unitOfWork.Save();
        }

        public void CreateTimer(TimerDTO timerDTO)
        {
            if(unitOfWork.Presets.GetByID(timerDTO.PresetId)==null)
            {
                throw new Exception("such preset doesn't exist");
            }
            unitOfWork.Timers.Create(timerDTO.ToTimer());
            unitOfWork.Save();
        }

        public void DeletePresetById(int presetid)
        {
            if (unitOfWork.Presets.GetByID(presetid) == null)
            {
                throw new Exception("such preset doesn't exist");
            }
            if (unitOfWork.Presets.GetByID(presetid).UserId == null)
            {
                throw new Exception("this preset is standard");
            }
            unitOfWork.Presets.Delete(presetid);
            unitOfWork.Save();
        }

        public PresetDTO GetPresetById(int presetid)
        {
            if (unitOfWork.Presets.GetByID(presetid) == null)
            {
                throw new Exception("such preset doesn't exist");
            }
            var preset = unitOfWork.Presets.GetByID(presetid);
            var timers = unitOfWork.Timers.GetAll();
            List<TimerDTO> timerDTOs = new List<TimerDTO>();
            foreach (var t in timers)
            {
                if (t.PresetId == preset.Id)
                {
                    timerDTOs.Add(t.ToTimerDTO());
                }
            }

            return preset.ToPresetDTO(timerDTOs);
        }

        public IQueryable<PresetDTO> GetAllCustomPresets(int userid)
        {
            var listOfPresetsDTO = new List<PresetDTO>();
            var presets = unitOfWork.Presets.GetAll();
            var timers = unitOfWork.Timers.GetAll();

            if (presets == null)
            {
                throw new NotImplementedException();
            }

            foreach (var p in presets)
            {
                List<TimerDTO> timerDTOs = new List<TimerDTO>();
                foreach (var t in timers)
                {
                    if (t.PresetId == p.Id)
                    {
                        timerDTOs.Add(t.ToTimerDTO());
                    }
                }
                if (p.UserId==userid)
                listOfPresetsDTO.Add(p.ToPresetDTO(timerDTOs));
            }

            return listOfPresetsDTO.AsQueryable();
        }

        public IQueryable<PresetDTO> GetAllStandardPresets()
        {
            var listOfPresetsDTO = new List<PresetDTO>();
            var presets = unitOfWork.Presets.GetAll();
            var timers = unitOfWork.Timers.GetAll();
            if (presets == null)
            {
                throw new NotImplementedException();
            }

            foreach (var p in presets)
            {
                List<TimerDTO> timerDTOs = new List<TimerDTO>();
                foreach (var t in timers)
                {
                    if (t.PresetId == p.Id)
                    {
                        timerDTOs.Add(t.ToTimerDTO());
                    }
                }
                if (p.UserId == null)
                {
                    listOfPresetsDTO.Add(p.ToPresetDTO(timerDTOs));
                }
            }

            return listOfPresetsDTO.AsQueryable();
        }

        public void DeleteTimer(int timerid)
        {
            unitOfWork.Timers.Delete(timerid);
            unitOfWork.Save();
        }
    }
}
