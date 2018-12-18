using System;
using System.Runtime.Serialization;

namespace Common.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        private const string UserExistExceptionMessage = "Incorrect password entered";

        public IncorrectPasswordException() : base(UserExistExceptionMessage)
        {
        }

        public IncorrectPasswordException(string message) : base(message)
        {
        }

        protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IncorrectPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
