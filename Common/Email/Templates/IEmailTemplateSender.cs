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
        /// <param name="details">The email message details. Note the Content property is ignored and replaced with the template</param>
        /// <param name="buttonUrl">The button URL</param>
        /// <returns></returns>
        Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details, string buttonUrl);
    }
}
