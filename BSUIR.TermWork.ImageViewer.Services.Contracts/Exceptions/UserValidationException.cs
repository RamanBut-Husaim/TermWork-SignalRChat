namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UserValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        public UserValidationException()
        {
        }

        public UserValidationException(string message)
            : base(message)
        {
        }

        public UserValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UserValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}