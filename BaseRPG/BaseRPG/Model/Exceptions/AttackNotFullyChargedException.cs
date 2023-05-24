using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.Exceptions
{
    [Serializable]
    internal class AttackNotFullyChargedException : Exception
    {
        public AttackNotFullyChargedException()
        {
        }

        public AttackNotFullyChargedException(string message) : base(message)
        {
        }

        public AttackNotFullyChargedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AttackNotFullyChargedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}