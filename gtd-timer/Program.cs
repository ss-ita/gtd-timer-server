//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace GtdTimer
{
    /// <summary>
    /// class for running a project
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Method to run project
        /// </summary>
        /// <param name="args">array for specific running parameters</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method for hosting project on web
        /// </summary>
        /// <param name="args">array for specific running parameters</param>
        /// <returns>web host builder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
