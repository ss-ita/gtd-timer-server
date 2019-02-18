//-----------------------------------------------------------------------
// <copyright file="LoginFailedException.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdCommon.ModelsDto;
using System;
using System.Runtime.Serialization;

namespace GtdCommon.Exceptions
{
    /// <summary>
    /// class for custom Login Failed Exception
    /// </summary>
    public class AlarmUpdateException : Exception
    {
        /// <summary>
        /// Exception error message
        /// </summary>
        private const string AlarmUpdateExceptionMessage = "Alarm record was already changed";
        private AlarmDto alarmDto = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        public AlarmUpdateException() : base(AlarmUpdateExceptionMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public AlarmUpdateException(AlarmDto alarmDto) : base(AlarmUpdateExceptionMessage)
        {
            this.alarmDto = alarmDto;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public AlarmUpdateException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        public AlarmUpdateException(string message, AlarmDto alarmDto) : base(message)
        {
            this.alarmDto = alarmDto;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        /// <param name="message">string message</param>
        /// <param name="innerException">inner exception</param>
        public AlarmUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFailedException" /> class.
        /// </summary>
        /// <param name="info">serialization info</param>
        /// <param name="context">streaming context</param>
        protected AlarmUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
