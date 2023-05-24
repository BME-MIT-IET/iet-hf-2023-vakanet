using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.Exceptions
{
    [Serializable]
    internal class NoSuchAttackBuilderException : Exception
    {
        public NoSuchAttackBuilderException()
        {
        }

        public NoSuchAttackBuilderException(string message) : base(message)
        {
        }

        public NoSuchAttackBuilderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchAttackBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}