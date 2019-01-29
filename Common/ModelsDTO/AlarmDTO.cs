﻿//-----------------------------------------------------------------------
// <copyright file="AlarmDto.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Xml.Serialization;

namespace GtdCommon.ModelsDto
{
    /// <summary>
    /// class for Alarm model
    /// </summary>
    public class AlarmDto
    {
        /// <summary>
        /// Gets or sets a value of Id property
        /// </summary>
        [XmlIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value of CronExpression property
        /// </summary>
        public string CronExpression { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property IsOn
        /// </summary>
        public bool IsOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property SoundOn
        /// </summary>
        public bool SoundOn { get; set; }

        /// <summary>
        /// Gets or sets a value of Message property
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value of user id foreign key property
        /// </summary>
        [XmlIgnore]
        public int UserId { get; set; }
    }
}
