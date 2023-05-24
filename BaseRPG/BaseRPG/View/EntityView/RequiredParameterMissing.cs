using System;
using System.Runtime.Serialization;

namespace BaseRPG.View.EntityView
{
    [Serializable]
    internal class RequiredParameterMissing : Exception
    {
        public RequiredParameterMissing()
        {
        }

        public RequiredParameterMissing(string message) : base(message)
        {
        }

        public RequiredParameterMissing(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequiredParameterMissing(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}