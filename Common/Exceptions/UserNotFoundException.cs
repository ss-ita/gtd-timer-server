//-----------------------------------------------------------------------
// <copyright file="UserNotFoundException.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace GtdCommon.Exceptions
{
    /// <summary>
    /// class for custom User Not Found Exception
    /// </summary>
    public class UserNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException" /> class.
        /// </summary>
        public UserNotFoundException() : base("User does not Exist!")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public UserNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="inner">inner exception</param>
        public UserNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotFoundException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        /// <summary>
        /// Gets or sets value of resource reference property
        /// </summary>
        public string ResourceReferenceProperty { get; set; }
    }
}
