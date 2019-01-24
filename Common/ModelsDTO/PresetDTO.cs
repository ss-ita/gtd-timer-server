//-----------------------------------------------------------------------
// <copyright file="PresetDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Preset model
    /// </summary>
    public class PresetDto
    {
        /// <summary>
        /// Gets or sets a value of Id property
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value of Preset name property
        /// </summary>
        public string PresetName { get; set; }

        /// <summary>
        /// Gets or sets a value of list of timers property
        /// </summary>
        public IEnumerable<TimerDto> Timers { get; set; }

        /// <summary>
        /// Gets or sets a value of user id foreign key property
        /// </summary>
        public int? UserId { get; set; }
    }
}