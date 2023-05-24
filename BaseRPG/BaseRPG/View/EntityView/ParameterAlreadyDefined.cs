using System;
using System.Runtime.Serialization;

namespace BaseRPG.View.EntityView
{
    [Serializable]
    internal class ParameterAlreadyDefined : Exception
    {
        public ParameterAlreadyDefined()
        {
        }

        public ParameterAlreadyDefined(string message) : base(message)
        {
        }

        public ParameterAlreadyDefined(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParameterAlreadyDefined(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}