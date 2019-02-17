﻿//-----------------------------------------------------------------------
// <copyright file="Record.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using GtdCommon.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of Record table
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Gets or sets Primary key for Record
        /// </summary>
        public int Id { get; set; }
   
        public string Name { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// Gets or sets Time when timer was started
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets Time when timer was stopped
        /// </summary>
        public DateTime StopTime { get; set; }

        /// <summary>
        /// Gets or sets what's happened with timer
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets how much time this record was runned
        /// </summary>
        public double ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets watch type column
        /// </summary>
        public WatchType WatchType { get; set; }

        /// <summary>
        /// Gets or sets Foreign key that reference to Task
        /// </summary>
        public int? TaskId { get; set; }

        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets Task that are referenced by this record
        /// </summary>
        public Tasks Task { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
