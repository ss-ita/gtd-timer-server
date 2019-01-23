//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of message table
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets Id column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Text column
        /// </summary>
        public string Text { get; set; }

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
