using System;
using System.Runtime.Serialization;

namespace BaseRPG.Controller.Window
{
    [Serializable]
    internal class NoSuchWindowException : Exception
    {
        public NoSuchWindowException()
        {
        }

        public NoSuchWindowException(string message) : base(message)
        {
        }

        public NoSuchWindowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchWindowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}