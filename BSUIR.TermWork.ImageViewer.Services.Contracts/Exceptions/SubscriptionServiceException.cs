namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SubscriptionServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        public SubscriptionServiceException()
        {
        }

        public SubscriptionServiceException(string message = DefaultMessage)
            : base(message)
        {
        }

        public SubscriptionServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SubscriptionServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}