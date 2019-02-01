//-----------------------------------------------------------------------
// <copyright file="XmlResult.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using GtdCommon.IoC;
using System;

namespace GtdTimer.ActionResults
{
    /// <summary>
    /// class for Extensible Markup Language text import and export
    /// </summary>
    public class XmlResult : FileResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlResult"/> class.
        /// </summary>
        /// <param name="objectToSerialize">The object to serialize to XML.</param>
        public XmlResult(object objectToSerialize) : base("text/xml")
        {
            this.ObjectToSerialize = objectToSerialize;
        }

        /// <summary>
        /// Gets the object to be serialized to XML.
        /// </summary>
        public object ObjectToSerialize { get; }

        /// <summary>
        /// Serializes the object that was passed into the constructor to XML and writes the corresponding XML to the result stream.
        /// </summary>
        /// <param name="context">The controller context for the current request.</param>       
        public override void ExecuteResult(ActionContext context)
        {
            if (this.ObjectToSerialize != null)
            {
                string cors = Environment.GetEnvironmentVariable("AzureCors") ?? IoCContainer.Configuration["Origins"];

                context.HttpContext.Response.Clear();
                var xmlSerializer = new System.Xml.Serialization.XmlSerializer(this.ObjectToSerialize.GetType());
                context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", cors);
                using (var writer = new StreamWriter(context.HttpContext.Response.Body, Encoding.UTF8))
                {
                    xmlSerializer.Serialize(writer, this.ObjectToSerialize);
                }              
            }
        }
    }
}
