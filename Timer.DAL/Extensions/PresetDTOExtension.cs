//-----------------------------------------------------------------------
// <copyright file="PresetDtoExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Extensions
{
    /// <summary>
    /// PresetDtoExtension class for converting to preset and vice versa
    /// </summary>
    public static class PresetDtoExtension
    {
        /// <summary>
        /// Convert to preset method
        /// </summary>
        /// <param name="presetDto"> presetDto model </param>
        /// <returns>returns preset</returns>
        public static Preset ToPreset(this PresetDto presetDto)
        {
            return new Preset
            {
                Name = presetDto.PresetName,
                Id = presetDto.Id,
                UserId = presetDto.UserId
            };
        }

        /// <summary>
        /// Convert to presetDto method
        /// </summary>
        /// <param name="preset">preset model</param>
        /// <param name="tasks">list of timers</param>
        /// <returns>returns presetDto</returns>
        public static PresetDto ToPresetDto(this Preset preset, List<TaskDto> tasks)
        {
            return new PresetDto
            {
                PresetName = preset.Name,
                Id = preset.Id,
                Tasks = tasks,
                UserId = preset.UserId
            };
        }
    }
}
