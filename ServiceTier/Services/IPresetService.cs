//-----------------------------------------------------------------------
// <copyright file="IPresetService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using GtdCommon.ModelsDto;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// interface for class preset service
    /// </summary>
    public interface IPresetService : IBaseService
    {
        /// <summary>
        /// Method for creating a preset
        /// </summary>
        /// <param name="presetDto"> preset model</param>
        void CreatePreset(PresetDto presetDto);

        /// <summary>
        /// Method for updating a preset
        /// </summary>
        /// <param name="presetDto"> preset model</param>
        void UpdatePreset(PresetDto presetDto);

        /// <summary>
        /// Method for deleting a preset
        /// </summary>
        /// <param name="presetid"> id of preset</param>
        void DeletePresetById(int presetid);

        /// <summary>
        /// Method for deleting a preset
        /// </summary>
        /// <param name="userid"> id of user</param>
        void DeleteAllPresetsByUserId(int userid);

        /// <summary>
        /// Method for obtaining a preset by id
        /// </summary>
        /// <param name="presetid"> id of preset</param>
        /// <returns> returns a preset with chosen id </returns>
        PresetDto GetPresetById(int presetid);

        /// <summary>
        /// Method for getting all standard presets
        /// </summary>
        /// <returns> list of all standard presets</returns>
        IList<PresetDto> GetAllStandardPresets();

        /// <summary>
        /// Method for getting all custom presets of chosen user
        /// </summary>
        /// <param name="userid"> user id </param>
        /// <returns> list of all custom presets of chosen user </returns>
        IList<PresetDto> GetAllCustomPresetsByUserId(int userid);
    }
}
