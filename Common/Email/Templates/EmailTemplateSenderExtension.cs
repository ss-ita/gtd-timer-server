//-----------------------------------------------------------------------
// <copyright file="EmailTemplateSenderExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace GtdCommon.Email.Templates
{
    /// <summary>
    /// Extension methods for any EmailTemplateSender classes
    /// </summary>
    public static class EmailTemplateSenderExtension
    {
        /// <summary>
        /// Injects the <see cref="EmailTemplateSender"/> into the services to handle the 
        /// <see cref="IEmailTemplateSender"/> service
        /// </summary>
        /// <param name="services">collection of services</param>
        /// <returns>collection for chaining</returns>
        public static IServiceCollection AddEmailTemplateSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailTemplateSender, EmailTemplateSender>();
            return services;
        }
    }
}
