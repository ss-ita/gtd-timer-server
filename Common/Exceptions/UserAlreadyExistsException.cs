using System;
using System.Runtime.Serialization;

namespace Common.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        private const string UserExistExceptionMessage = "User with such email address already exists";

        public UserAlreadyExistsException() : base(UserExistExceptionMessage)
        {
        }

        public UserAlreadyExistsException(string message) : base(message)
        {
        }

        protected UserAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
