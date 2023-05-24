using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.Exceptions
{
    [Serializable]
    internal class ValueLockedException : Exception
    {
        public ValueLockedException()
        {
        }

        public ValueLockedException(string message) : base(message)
        {
        }

        public ValueLockedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueLockedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}