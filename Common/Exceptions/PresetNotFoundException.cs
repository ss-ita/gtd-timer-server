//-----------------------------------------------------------------------
// <copyright file="PresetNotFoundException.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace GtdCommon.Exceptions
{
    /// <summary>
    /// class for custom Preset Not Found Exception
    /// </summary>
    public class PresetNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PresetNotFoundException" /> class.
        /// </summary>
        public PresetNotFoundException() : base("Preset does not Exist!")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PresetNotFoundException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public PresetNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PresetNotFoundException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="inner">inner exception</param>
        public PresetNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PresetNotFoundException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected PresetNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }

        /// <summary>
        /// Gets or sets value of resource reference property
        /// </summary>
        public string ResourceReferenceProperty { get; set; }
    }
}
