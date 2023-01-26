using System;
using System.Runtime.Serialization;

namespace SmevTransform.NET.Exceptions
{
    [Serializable]
    internal class TransformationException : Exception
    {
        public TransformationException()
        {
        }

        public TransformationException(string message) : base(message)
        {
        }

        public TransformationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public override string Message => $"{base.Message}\n\n{InnerException?.Message}";

        protected TransformationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
