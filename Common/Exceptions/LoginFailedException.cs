//-----------------------------------------------------------------------
// <copyright file="LoginFailedException.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace GtdCommon.Exceptions
{
    /// <summary>
    /// class for custom Login Failed Exception
    /// </summary>
    public class LoginFailedException : Exception
    {
        /// <summary>
        /// Exception error message
        /// </summary>
        private const string LoginFailedExceptionnMessage = "Incorrect credentials entered";

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        public LoginFailedException() : base(LoginFailedExceptionnMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public LoginFailedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="innerException">inner exception</param>
        public LoginFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected LoginFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
