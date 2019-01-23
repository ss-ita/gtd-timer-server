//-----------------------------------------------------------------------
// <copyright file="IoCContainer.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace GtdCommon.IoC
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// class for adding inversion of control in project
    /// </summary>
    public static class IoCContainer
    {
        /// <summary>
        /// Gets or sets a value of Configuration property
        /// </summary>
        public static IConfiguration Configuration { get; set; }
    }
}
