﻿//-----------------------------------------------------------------------
// <copyright file="TaskRecordDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Xml.Serialization;

using GtdCommon.Constant;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for TaskRecord model
    /// </summary>
    [Serializable, XmlRoot("TaskRecordDto")]
    public class TaskRecordDto
    {
        /// <summary>
        /// Gets or sets a value of Id property
        /// </summary>
        [XmlIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value of Name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value of Description value
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value of Action value
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets a value of StartTime value
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets a value of StopTime value
        /// </summary>
        public DateTime StopTime { get; set; }

        /// <summary>
        /// Gets or sets a value of ElapsedTime value
        /// </summary>
        public double ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets of type of record
        /// </summary>
        public WatchType WatchType { get; set; }

        /// <summary>
        /// Gets or sets a value of TaskId value
        /// </summary>
        public int? TaskId { get; set; }

        /// <summary>
        /// Gets or sets a value of UserId value
        /// </summary>
        public int UserId { get; set; }
    }
}
