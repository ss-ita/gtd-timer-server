//-----------------------------------------------------------------------
// <copyright file="SendEmailDetails.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdCommon.Email
{
    /// <summary>
    /// The details about email to send
    /// </summary>
    public class SendEmailDetails
    {
        /// <summary>
        /// Gets or sets the name of the sender
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the email of the sender
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the receiver
        /// </summary>
        public string ToName { get; set; }

        /// <summary>
        /// Gets or sets the email of the receiver
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// Gets or sets the email subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email body content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the contents is a HTML email
        /// </summary>
        public bool IsHTML { get; set; }
    }
}
