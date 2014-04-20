using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal sealed class AccessRightValidator : EntityValidator<AccessRight>
    {
        public AccessRightValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Description, this.ValidateDescription);
        }

        #region Overrides of EntityValidator<AccessRight>

        public override void Validate(AccessRight entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.ValidateKey(entity.Key);
            this.ValidateDescription(entity.Description);
        }

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
