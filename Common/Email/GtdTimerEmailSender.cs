using System.Threading.Tasks;
using GtdCommon.IoC;

namespace GtdCommon.Email
{
    public class GtdTimerEmailSender
    {
        public static async Task<SendEmailResponse> SendUserVerificationEmailAsync(string displayName, string email, string verificationUrl)
        {
            return await IoCContainer.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
            {
                Content = "This is our first email",
                IsHTML = true,
                FromEmail = "gtdTimer@gmail.com",
                FromName = "GtdTimer",
                ToEmail = "yuras.nazar666@gmail.com",
                ToName = "Some User",
                Subject = "This is sent from gtdTimer"
            }, verificationUrl);
        }
    }
}
