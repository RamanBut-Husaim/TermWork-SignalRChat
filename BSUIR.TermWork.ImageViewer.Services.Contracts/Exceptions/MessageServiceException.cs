using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    [Serializable]
    public sealed class MessageServiceException : EntityValidationException
    {
        public MessageServiceException()
        {
        }

        public MessageServiceException(string message) : base(message)
        {
        }

        public MessageServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MessageServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
