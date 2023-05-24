using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.Exceptions
{
    [Serializable]
    internal class RequiredArgNotSetException : Exception
    {
        public RequiredArgNotSetException()
        {
        }

        public RequiredArgNotSetException(string message) : base(message)
        {
        }

        public RequiredArgNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequiredArgNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}