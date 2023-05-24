using System;
using System.Runtime.Serialization;

namespace BaseRPG.Model.Exceptions
{
    [Serializable]
    internal class InvalidSkillCastParamsException : Exception
    {
        public InvalidSkillCastParamsException()
        {
        }

        public InvalidSkillCastParamsException(Type type, Type templateType) :
            base("\"" + type.Name + "\" type is not the same as the template parameter type (\"" + templateType.Name + "\")")
        {

        }

        public InvalidSkillCastParamsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidSkillCastParamsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}