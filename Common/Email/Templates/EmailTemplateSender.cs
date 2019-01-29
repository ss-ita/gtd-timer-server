using GtdCommon.IoC;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GtdCommon.Email.Templates
{
    public class EmailTemplateSender : IEmailTemplateSender
    {
        public async Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details, string buttonUrl)
        {
            var templateText = default(string);

            // Read the general template from file
            // TODO: Replace with IoC Flat data provider
            using (var reader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream("GtdTimer.EmailTemplate.htm"), Encoding.UTF8))
            {
                // Read file contents
                templateText = await reader.ReadToEndAsync();
            }

            templateText = templateText.Replace("--ButtonUrl--", buttonUrl);
            // Set the details content to this template content
            details.Content = templateText;
            

            return await IoCContainer.EmailSender.SendEmailAsync(details);

        }
    }
}
