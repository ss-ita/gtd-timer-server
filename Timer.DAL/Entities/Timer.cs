//-----------------------------------------------------------------------
// <copyright file="Timer.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of timer table
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// Gets or sets Id column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Name column
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Interval column
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <summary>
        /// Gets or sets PresetId column
        /// </summary>
        public int PresetId { get; set; }

        /// <summary>
        /// Gets or sets Foreign key to preset table
        /// </summary>
        public Preset Preset { get; set; }
    }
}
