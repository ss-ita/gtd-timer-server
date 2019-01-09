using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Exceptions
{
    public class StandartPresetException : Exception
    {
        public string ResourceReferenceProperty { get; set; }

        public StandartPresetException() : base("This preset is standard")
        {

        }
        public StandartPresetException(string message) : base(message)
        {

        }
        public StandartPresetException(string message, Exception inner) : base(message, inner)
        {

        }
        protected StandartPresetException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ResourceReferenceProperty = info.GetString("ResourceReferenceProperty");
        }
    }
}
