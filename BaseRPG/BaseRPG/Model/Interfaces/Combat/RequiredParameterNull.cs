using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.Interfaces.Combat
{
    [Serializable]
    internal class RequiredParameterNull : Exception
    {
        public RequiredParameterNull()
        {
        }

        public RequiredParameterNull(string message) : base(message)
        {
        }

        public RequiredParameterNull(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequiredParameterNull(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}