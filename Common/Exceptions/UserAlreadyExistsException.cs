//-----------------------------------------------------------------------
// <copyright file="UserAlreadyExistsException.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace GtdCommon.Exceptions
{
    /// <summary>
    /// class for custom User Already Exists Exception
    /// </summary>
    public class UserAlreadyExistsException : Exception
    {
        /// <summary>
        /// Exception error message
        /// </summary>
        private const string UserExistExceptionMessage = "User with such email address already exists";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsException" /> class.
        /// </summary>
        public UserAlreadyExistsException() : base(UserExistExceptionMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public UserAlreadyExistsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="innerException">inner exception</param>
        public UserAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAlreadyExistsException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected UserAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
