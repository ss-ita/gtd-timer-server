//-----------------------------------------------------------------------
// <copyright file="IEmailTemplateSender.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Threading.Tasks;

namespace GtdCommon.Email.Templates
{
    /// <summary>
    /// Sends emails using the <see cref="IEmailSender"/> and creating the HTML
    /// email from specific templates
    /// </summary>
    public interface IEmailTemplateSender
    {
        /// <summary>
        /// Sends an email with the given details using the General template
        /// </summary>
        /// <param name="details">the email message details</param>
        /// <param name="buttonUrl">The button URL</param>
        /// <returns>return result of sending email</returns>
        Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details, string buttonUrl);
    }
}
