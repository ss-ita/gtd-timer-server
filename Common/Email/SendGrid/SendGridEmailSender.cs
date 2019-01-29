using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GtdCommon.Email.SendGrid
{
    public class SendGridEmailSender : IEmailSender
    {
        public async Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details)
        {

            // Get the SendGrid key.  Create a new SendGrid client
            var client = new SendGridClient("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");
            // From details
            var from = new EmailAddress(details.FromEmail, details.FromName);
            // To details
            var to = new EmailAddress(details.ToEmail, details.ToName);
            // Subject 
            var subject = details.Subject;
            // Content
            var Content = details.Content;
            // Create Email class ready to send
            var msg = MailHelper.CreateSingleEmail(
                                                   from,
                                                   to,
                                                   subject,
                                                   //Plain Content
                                                   details.IsHTML ? null : details.Content,
                                                   //HTML Content
                                                   details.IsHTML ? details.Content : null);


            // Finally, send the email...
            var response = await client.SendEmailAsync(msg);

            // If we succeeded...
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                // Return successful response
                return new SendEmailResponse();

            // Otherwise, it failed...

            try
            {
                // Get the result in the body
                var bodyResult = await response.Body.ReadAsStringAsync();

                // Deserialize the response
                var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(bodyResult);

                // Add any errors to the response
                var errorResponse = new SendEmailResponse
                {
                    Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
                };

                // Make sure we have at least one error
                if (errorResponse.Errors == null || errorResponse.Errors.Count == 0)
                    // Add an unknown error
                    // TODO: Localization
                    errorResponse.Errors = new List<string>(new[] { "Unknown error from email sending service. Please contact Fasetto support." });

                // Return the response
                return errorResponse;

            }
            catch (Exception ex)
            {
                // TODO: Localization

                // Break if we are debugging
                if (Debugger.IsAttached)
                {
                    var error = ex;
                    Debugger.Break();
                }

                // If something unexpected happened, return message
                return new SendEmailResponse
                {
                    Errors = new List<string>(new[] { "Unknown error occurred" })
                };
            }
        }
    }
}
