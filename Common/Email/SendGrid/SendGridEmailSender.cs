//-----------------------------------------------------------------------
// <copyright file="SendGridEmailSender.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;

namespace GtdCommon.Email.SendGrid
{
    /// <summary>
    /// Sends emails using the SendGrid service
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        /// <summary>
        /// Send the email
        /// </summary>
        /// <param name="details">The email message details/param>
        /// <returns>return result of sending email</returns>
        public async Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress(details.FromEmail, details.FromName);
            var to = new EmailAddress(details.ToEmail, details.ToName);
            var subject = details.Subject;
            var Content = details.Content;
            var msg = MailHelper.CreateSingleEmail(
                                                   from,
                                                   to,
                                                   subject,
                                                   details.IsHTML ? null : details.Content,
                                                   details.IsHTML ? details.Content : null);


            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                return new SendEmailResponse();

            try
            {
                var bodyResult = await response.Body.ReadAsStringAsync();
                var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(bodyResult);
                var errorResponse = new SendEmailResponse
                {
                    Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
                };

                if (errorResponse.Errors == null || errorResponse.Errors.Count == 0)
                    errorResponse.Errors = new List<string>(new[] { "Unknown error from email sending service. Please contact Fasetto support." });

                return errorResponse;

            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    var error = ex;
                    Debugger.Break();
                }

                return new SendEmailResponse
                {
                    Errors = new List<string>(new[] { "Unknown error occurred" })
                };
            }
        }
    }
}
