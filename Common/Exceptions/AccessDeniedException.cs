//-----------------------------------------------------------------------
// <copyright file="AccessDeniedException.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace GtdCommon.Exceptions
{
    /// <summary>
    /// class for custom Access Denied Exception
    /// </summary>
    public class AccessDeniedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException" /> class.
        /// </summary>
        public AccessDeniedException() : base("You are not the owner of this preset!")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public AccessDeniedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="inner">inner exception</param>
        public AccessDeniedException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected AccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        /// <summary>
        /// Gets or sets value of resource reference property
        /// </summary>
        public string ResourceReferenceProperty { get; set; }
    }
}
