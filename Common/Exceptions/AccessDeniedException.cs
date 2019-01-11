using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public AccessDeniedException() : base("You are not the owner of this preset!")
        {

        }
        public AccessDeniedException(string message) : base(message)
        {

        }
        public AccessDeniedException(string message, Exception inner) : base(message, inner)
        {

        }
        protected AccessDeniedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }
    }
}
