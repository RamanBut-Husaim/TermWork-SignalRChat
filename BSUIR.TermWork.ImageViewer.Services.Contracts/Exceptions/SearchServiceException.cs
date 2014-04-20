namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SearchServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        public SearchServiceException()
        {
        }

        public SearchServiceException(string message = DefaultMessage)
            : base(message)
        {
        }

        public SearchServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SearchServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}