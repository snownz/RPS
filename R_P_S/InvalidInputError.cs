using System;
using System.Runtime.Serialization;

namespace R_P_S
{
    [Serializable]
    internal class InvalidInputError : Exception
    {
        public InvalidInputError()
        {
        }

        public InvalidInputError(string message) : base(message)
        {
        }

        public InvalidInputError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidInputError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}