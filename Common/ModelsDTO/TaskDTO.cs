//-----------------------------------------------------------------------
// <copyright file="TaskDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Task model
    /// </summary>
    public class TaskDto
    {
        /// <summary>
        /// Gets or sets a value of Id property
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value of Name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value of Description property
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value of Elapsed time property
        /// </summary>
        public long ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets a value of last start time property
        /// </summary>
        public DateTime LastStartTime { get; set; }

        /// <summary>
        /// Gets or sets a value of goal property
        /// </summary>
        public TimeSpan? Goal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property is running
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Gets or sets a value of user id foreign key property
        /// </summary>
        public int UserId { get; set; }
    }
}
