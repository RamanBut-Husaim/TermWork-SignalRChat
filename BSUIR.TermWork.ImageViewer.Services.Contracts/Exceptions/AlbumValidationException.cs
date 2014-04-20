namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AlbumValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        public AlbumValidationException()
        {
        }

        public AlbumValidationException(string message)
            : base(message)
        {
        }

        public AlbumValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AlbumValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}