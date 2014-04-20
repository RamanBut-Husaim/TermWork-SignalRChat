using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal sealed class RoleValidator : EntityValidator<Role>
    {
        #region Overrides of EntityValidator<Role>

        public RoleValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Description, this.ValidateDescription);
        }

        public override void Validate(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            this.ValidateKey(role.Key);
            this.ValidateDescription(role.Description);
        }

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
