//-----------------------------------------------------------------------
// <copyright file="Alarm.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Alarm table
    /// </summary>
    public class Alarm
    {
        /// <summary>
        /// Gets or sets Id column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets CronExpression column
        /// </summary>
        public string CronExpression { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether it is IsTOn or of
        /// </summary>
        public bool IsOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SoundOn on or of
        /// </summary>
        public bool SoundOn { get; set; }

        /// <summary>
        /// Gets or sets Message column
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets Timestamp column
        /// </summary>
        [Timestamp]
        public byte[] Timestamp { get; set; }

        /// <summary>
        /// Gets or sets UserId column
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets Foreign key to user table
        /// </summary>
        public User User { get; set; }
    }
}
