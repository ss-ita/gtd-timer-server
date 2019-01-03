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

            Preset preset = new Preset
            {
                Name = presetDTO.PresetName,
                Id = presetDTO.Id,
                UserId = presetDTO.UserId
            };

            return preset;
        }

        public static PresetDTO ToPresetDTO(this Preset preset,List<TimerDTO> timers)
        {
            PresetDTO presetDTO = new PresetDTO
            {
               PresetName=preset.Name,
               Id=preset.Id,
               Timers=timers,
               UserId=preset.UserId
            };

            return presetDTO;
        }
    }
}
