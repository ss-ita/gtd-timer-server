//-----------------------------------------------------------------------
// <copyright file="TaskNotFoundException.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace GtdCommon.Exceptions
{
    /// <summary>
    /// class for custom Task Not Found Exception
    /// </summary>
    public class TaskNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotFoundException" /> class.
        /// </summary>
        public TaskNotFoundException() : base("Task does not exist!")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotFoundException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public TaskNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotFoundException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="inner">inner exception</param>
        public TaskNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotFoundException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected TaskNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        /// <summary>
        /// Gets or sets value of resource reference property
        /// </summary>
        public string ResourceReferenceProperty { get; set; }
    }
}
