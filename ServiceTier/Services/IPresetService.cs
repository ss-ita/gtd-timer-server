using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ModelsDTO;
using Timer.DAL.Timer.DAL.Entities;

namespace ServiceTier.Services
{
    public interface IPresetService : IBaseService
    {
        void CreatePreset(PresetDTO presetDTO);
        void CreateTimer(TimerDTO timerDTO);
        void UpdatePreset(PresetDTO presetDTO);
        void UpdateTimer(TimerDTO timerDTO);
        void DeleteTimer(int timerid);
        void DeletePresetById(int presetid);
        PresetDTO GetPresetById(int presetid);
        IQueryable<PresetDTO> GetAllStandardPresets();
        IQueryable<PresetDTO> GetAllCustomPresets(int userid);
    }
}
