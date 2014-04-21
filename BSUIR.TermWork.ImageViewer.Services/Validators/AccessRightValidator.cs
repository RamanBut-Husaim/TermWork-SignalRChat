// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessRightValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The access right validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The access right validator.
    /// </summary>
    internal sealed class AccessRightValidator : EntityValidator<AccessRight>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessRightValidator"/> class.
        /// </summary>
        public AccessRightValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Description, this.ValidateDescription);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public override void Validate(AccessRight entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.ValidateKey(entity.Key);
            this.ValidateDescription(entity.Description);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The validate description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <exception cref="AccessRightValidationException">
        /// </exception>
        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new AccessRightValidationException(
                    "The desciption property is null or empty.");
            }

            if (description.Length > Role.MaxLengthFor.Description)
            {
                throw new AccessRightValidationException("The description property is too long.");
            }
        }

        #endregion
    }
}