//-----------------------------------------------------------------------
// <copyright file="IoCContainer.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdCommon.Email;
using GtdCommon.Email.Templates;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GtdCommon.IoC
{
    /// <summary>
    /// The dependency injection container making use of the built in .Net Core service provider
    /// </summary>
    public static class IoCContainer
    {
        /// <summary>
        /// Gets or sets the configuration manager for the application
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets or sets the application builder for the application
        /// </summary>
        public static IApplicationBuilder AppBuilder { get; set; }

        /// <summary>
        /// Gets or sets the transient instance of the <see cref="IEmailSender"/>
        /// </summary>
        public static IEmailSender EmailSender => AppBuilder.ApplicationServices.GetService<IEmailSender>();

        /// <summary>s
        /// The transient instance of the <see cref="IEmailTemplateSender"/>
        /// </summary>
        public static IEmailTemplateSender EmailTemplateSender => AppBuilder.ApplicationServices.GetService<IEmailTemplateSender>();
    }
}
