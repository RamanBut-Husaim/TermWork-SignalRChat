using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    [Serializable]
    public sealed class MessageValidationException : EntityValidationException
    {
        public MessageValidationException()
        {
        }

        public MessageValidationException(string message) : base(message)
        {
        }

        public MessageValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MessageValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
