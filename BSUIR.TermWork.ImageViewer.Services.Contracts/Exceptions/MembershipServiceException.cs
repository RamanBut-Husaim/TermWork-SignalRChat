namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class MembershipServiceException : EntityValidationException
    {
        #region Constructors and Destructors

        public MembershipServiceException()
        {
        }

        public MembershipServiceException(string message = DefaultMessage)
            : base(message)
        {
        }

        public MembershipServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MembershipServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}