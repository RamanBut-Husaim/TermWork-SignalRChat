namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AccessRightValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        public AccessRightValidationException()
        {
        }

        public AccessRightValidationException(string message)
            : base(message)
        {
        }

        public AccessRightValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AccessRightValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}