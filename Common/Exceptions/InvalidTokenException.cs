//-----------------------------------------------------------------------
// <copyright file="InvalidTokenException.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace GtdCommon.Exceptions
{
    /// <summary>
    /// class for custom Invalid Token Exception
    /// </summary>
    public class InvalidTokenException : Exception
    {
        /// <summary>
        /// Exception error message
        /// </summary>
        private const string InvalidTokenExceptionMessage = "Invalid Token!";


        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTokenException" /> class.
        /// </summary>
        public InvalidTokenException() : base(InvalidTokenExceptionMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTokenException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public InvalidTokenException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTokenException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="innerException">inner exception</param>
        public InvalidTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTokenException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected InvalidTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
