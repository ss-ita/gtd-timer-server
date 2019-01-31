//-----------------------------------------------------------------------
// <copyright file="SendGridResponse.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace GtdCommon.Email.SendGrid
{
    /// <summary>
    /// A response to a SendGrid SendMessage call
    /// </summary>
    public class SendGridResponse
    {
        /// <summary>
        /// Any errors from a response
        /// </summary>
        public List<SendGridResponseError> Errors { get; set; }
    }
}
