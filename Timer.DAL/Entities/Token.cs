//-----------------------------------------------------------------------
// <copyright file="Token.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace GtdTimerDAL.Entities
{
    /// <summary>
    /// Fields of tokens table
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Gets or sets Id column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets UserId column
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets token value
        /// </summary>
        public string TokenValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when token is creating
        /// </summary>
        public DateTime TokenCreationTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating when token will expire
        /// </summary>
        public DateTime TokenExpirationTime { get; set; }
    }
}
