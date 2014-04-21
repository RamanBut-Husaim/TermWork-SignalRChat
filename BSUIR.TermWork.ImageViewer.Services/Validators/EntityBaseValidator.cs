// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityBaseValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The entity base validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections.Generic;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The entity base validator.
    /// </summary>
    internal abstract class EntityBaseValidator
    {
        #region Static Fields

        /// <summary>
        /// The operation buffer.
        /// </summary>
        private static readonly Dictionary<string, object> operationBuffer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="EntityBaseValidator"/> class.
        /// </summary>
        static EntityBaseValidator()
        {
            operationBuffer = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the operation buffer.
        /// </summary>
        protected Dictionary<string, object> OperationBuffer
        {
            get { return operationBuffer; }
        }

        #endregion
    }
}