using System;
using System.Runtime.Serialization;

namespace gtdtimer.Extentions.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public UserNotFoundException() : base("User does not Exist!")
        {

        }
        public UserNotFoundException(string message) : base(message)
        {

        }
        public UserNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
        protected UserNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }
    }
}
