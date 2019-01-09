using System;
using System.Runtime.Serialization;

namespace Common.Exceptions
{
    public class LoginFailedException : Exception
    {
        private const string LoginFailedExceptionnMessage = "Incorrect credentials entered";

        public LoginFailedException() : base(LoginFailedExceptionnMessage)
        {
        }

        public LoginFailedException(string message) : base(message)
        {
        }

        protected LoginFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public LoginFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
