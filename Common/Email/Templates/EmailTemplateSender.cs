//-----------------------------------------------------------------------
// <copyright file="EmailTemplateSender.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using GtdCommon.IoC;

namespace GtdCommon.Email.Templates
{
    /// <summary>
    /// Handles sending templated emails
    /// </summary>
    public class EmailTemplateSender : IEmailTemplateSender
    {
        /// <summary>
        /// Read the general template and send email
        /// </summary>
        /// <param name="details">email message details</param>
        /// <param name="buttonUrl">the button URL</param>
        /// <param name="buttonText">Text on button</param>
        /// <param name="titleMessage">Title text</param>
        /// <param name="mainMessage">The main message of email</param>
        /// <returns>response of sending email</returns>
        public async Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details, string buttonUrl, string buttonText, string titleMessage, string mainMessage)
        {
            var templateText = default(string);
          
            using (var reader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("GtdTimer.EmailTemplate.html"), Encoding.UTF8))
            {
                templateText = await reader.ReadToEndAsync();
            }

            templateText = templateText.Replace("--ButtonUrl--", buttonUrl);
            templateText = templateText.Replace("--ButtonText--", buttonText);
            templateText = templateText.Replace("--TitleMessage--", titleMessage);
            templateText = templateText.Replace("--MainMessage--", mainMessage);

            details.Content = templateText;

            return await IoCContainer.EmailSender.SendEmailAsync(details);
        }
    }
}
