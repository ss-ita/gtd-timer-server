//-----------------------------------------------------------------------
// <copyright file="CsvResult.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using GtdCommon.IoC;
using GtdCommon.ModelsDto;
using ServiceStack.Text;

namespace GtdTimer.ActionResults
{
    /// <summary>
    /// Class for coma separated value import and export
    /// </summary>
    public class CsvResult : FileResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvResult"/> class.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize to Csv.</param>
        public CsvResult(object objectToSerialize) : base("text/csv")
        {
            this.ObjectToSerialize = objectToSerialize;
        }

        /// <summary>
        /// Gets the object to be serialized to Csv.
        /// </summary>
        public object ObjectToSerialize { get; }

        /// <summary>
        /// Serializes the object that was passed into the constructor to Csv and writes the corresponding Csv to the result stream.
        /// </summary>
        /// <param name="context">The controller context for the current request.</param>       
        public override void ExecuteResult(ActionContext context)
        {
            string cors = Environment.GetEnvironmentVariable("AzureCors") ?? IoCContainer.Configuration["Origins"];

            if (this.ObjectToSerialize != null)
            {
                context.HttpContext.Response.Clear();               
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", cors);
                JsConfig<TaskDto>.ExcludePropertyNames = new[] { "Id", "UserId" };
                JsConfig<TaskRecordDto>.ExcludePropertyNames = new[] { "Id" };
                using (var writer = new StreamWriter(context.HttpContext.Response.Body, Encoding.UTF8))
                {
                    CsvSerializer.SerializeToWriter(ObjectToSerialize, writer);                   
                }
            }
        }
    }
}
