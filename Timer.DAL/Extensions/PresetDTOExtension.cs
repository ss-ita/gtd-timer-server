using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace Timer.DAL.Extensions
{
    public static class PresetDTOExtension
    {
        public static Preset ToPreset(this PresetDTO presetDTO)
        {
            return new Preset
            {
                Name = presetDTO.PresetName,
                Id = presetDTO.Id,
                UserId = presetDTO.UserId
            };
        }

        public static PresetDTO ToPresetDTO(this Preset preset,List<TimerDTO> timers)
        {
            return new PresetDTO
            {
                PresetName = preset.Name,
                Id = preset.Id,
                Timers = timers,
                UserId = preset.UserId
            };
        }
    }
}
