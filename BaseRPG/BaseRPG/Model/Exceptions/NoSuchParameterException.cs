using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.Exceptions
{
    [Serializable]
    internal class NoSuchParameterException : Exception
    {
        public NoSuchParameterException()
        {
        }

        public NoSuchParameterException(string message) : base(message)
        {
        }

        public NoSuchParameterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}