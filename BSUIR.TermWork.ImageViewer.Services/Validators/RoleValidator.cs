// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The role validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The role validator.
    /// </summary>
    internal sealed class RoleValidator : EntityValidator<Role>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleValidator"/> class.
        /// </summary>
        public RoleValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Description, this.ValidateDescription);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public override void Validate(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.ValidateKey(role.Key);
            this.ValidateDescription(role.Description);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The validate description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <exception cref="RoleValidationException">
        /// </exception>
        private void ValidateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new RoleValidationException("The role property is null or empty.");
            }

            if (description.Length > Role.MaxLengthFor.Description)
            {
                throw new RoleValidationException("The role property is too long.");
            }
        }

        #endregion
    }
}