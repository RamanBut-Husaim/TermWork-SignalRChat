namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ProfileValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        public ProfileValidationException()
        {
        }

        public ProfileValidationException(string message)
            : base(message)
        {
        }

        public ProfileValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProfileValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}