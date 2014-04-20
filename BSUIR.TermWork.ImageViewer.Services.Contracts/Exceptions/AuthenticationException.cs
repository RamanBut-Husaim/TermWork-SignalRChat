namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class AuthenticationException : MembershipServiceException
    {
        #region Constructors and Destructors

        public AuthenticationException()
        {
        }

        public AuthenticationException(string message)
            : base(message)
        {
        }

        public AuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AuthenticationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}