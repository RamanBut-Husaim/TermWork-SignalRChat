namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ImageValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        public ImageValidationException()
        {
        }

        public ImageValidationException(string message)
            : base(message)
        {
        }

        public ImageValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ImageValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}