// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleValidationException.cs" company="">
//   
// </copyright>
// <summary>
//   The role validation exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Runtime.Serialization;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions
{
    /// <summary>
    /// The role validation exception.
    /// </summary>
    [Serializable]
    public class RoleValidationException : EntityValidationException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleValidationException"/> class.
        /// </summary>
        public RoleValidationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public RoleValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleValidationException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public RoleValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleValidationException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected RoleValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}