//-----------------------------------------------------------------------
// <copyright file="Tasks.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using GtdCommon.Constant;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of task table
    /// </summary>
    public class Tasks
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
        /// Gets or sets Description column
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets ElapsedTime column
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets LastStartTime column
        /// </summary>
        public DateTime LastStartTime { get; set; }

        /// <summary>
        /// Gets or sets Goal column
        /// </summary>
        public TimeSpan? Goal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether task IsRunning
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Gets or sets UserId column
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets Foreign key to user table
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets watch type column
        /// </summary>
        public WatchType WatchType { get; set; }
    }
}
