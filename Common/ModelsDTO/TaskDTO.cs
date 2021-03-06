﻿//-----------------------------------------------------------------------
// <copyright file="TaskDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Xml.Serialization;
using GtdCommon.Constant;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Task model
    /// </summary>
    [Serializable, XmlRoot("TaskDto")]
    public class TaskDto
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
        /// Gets or sets a value indicating whether property is running
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Gets or sets a value of user id foreign key property
        /// </summary>
        [XmlIgnore]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property is running
        /// </summary>
        public WatchType WatchType { get; set; }
    }
}