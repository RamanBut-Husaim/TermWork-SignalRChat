namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    using BSUIR.TermWork.ImageViewer.Shared.Exceptions;

    [Serializable]
    public class EntityValidationException : ImageViewerException
    {
        #region Constants

        public const string DefaultMessage = "The operation cannot be completed. See inner exception for more details.";

        #endregion

        #region Constructors and Destructors

        public EntityValidationException()
        {
        }

        public EntityValidationException(string message)
            : base(message)
        {
        }

        public EntityValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EntityValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}