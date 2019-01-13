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
        void UpdatePreset(PresetDTO presetDTO);
        void DeletePresetById(int presetid);
        PresetDTO GetPresetById(int presetid);
        IList<PresetDTO> GetAllStandardPresets();
        IList<PresetDTO> GetAllCustomPresetsByUserId(int userid);
    }
}
