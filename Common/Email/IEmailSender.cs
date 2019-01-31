//-----------------------------------------------------------------------
// <copyright file="IEmailSender.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Threading.Tasks;

namespace GtdCommon.Email
{
    /// <summary>
    /// A service that handles sending emails on behalf of the caller
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Send an email message with the given information
        /// </summary>
        /// <param name="details">The details about email to send</param>
        /// <returns>return result of sending email</returns>
        Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}
