//-----------------------------------------------------------------------
// <copyright file="SendGridEmailExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace GtdCommon.Email.SendGrid
{
    /// <summary>
    /// Extension methods for any SendGrid classes
    /// </summary>
    public static class SendGridExtension
    {
        /// <summary>
        /// Injects the <see cref="SendGridEmailSender"/> into the services to handle the 
        /// <see cref="IEmailSender"/> service
        /// </summary>
        /// <param name="services">collection of services</param>
        /// <returns>collection for chaining</returns>
        public static IServiceCollection AddSendGridEmailSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, SendGridEmailSender>();
            return services;
        }
    }
}
