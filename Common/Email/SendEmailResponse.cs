﻿//-----------------------------------------------------------------------
// <copyright file="SendEmailResponse.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace GtdCommon.Email
{
    /// <summary>
    /// A response from a SendEmail call for any <see cref="IEmailSender"/> implementation
    /// </summary>
    public class SendEmailResponse
    {
        /// <summary>
        /// True if the email was sent successfully
        /// </summary>
        public bool Successful => !(this.Errors?.Count > 0);

        /// <summary>
        /// Gets or sets the error message if the sending failed
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
