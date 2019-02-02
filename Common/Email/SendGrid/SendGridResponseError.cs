//-----------------------------------------------------------------------
// <copyright file="SendGridResponseError.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdCommon.Email.SendGrid
{
    /// <summary>
    /// An error response for a <see cref="SendGridResponse"/>
    /// </summary>
    public class SendGridResponseError
    {
        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets еhe field inside the email message details that the error is related to
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets useful information for resolving the error
        /// </summary>
        public string Help { get; set; }
    }
}
