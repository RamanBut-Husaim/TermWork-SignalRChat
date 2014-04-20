using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal sealed class ProfileValidator : EntityValidator<Profile>
    {
        public ProfileValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.FirstName, this.ValidateFirstName);
            this.RegisterProperty(p => p.LastName, this.ValidateLastName);
            this.RegisterProperty(p => p.LastSignIn, this.ValidateDate);
            this.RegisterProperty(p => p.LastSignOut, this.ValidateDate);
            this.RegisterProperty(p => p.RegistrationDate, this.ValidateDate);
        }

        #region Overrides of EntityValidator<Profile>

        public override void Validate(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }
            this.ValidateKey(profile.Key);
            this.ValidateFirstName(profile.FirstName);
            this.ValidateLastName(profile.LastName);
        }

        #endregion

        private void ValidateFirstName(string firstName)
        {
            this.ValidateName(firstName, "First name", Profile.MaxLengthFor.FirstName);
        }

        private void ValidateLastName(string lastName)
        {
            this.ValidateName(lastName, "Last name", Profile.MaxLengthFor.LastName);
        }

        private void ValidateName(string name, string fieldName, int fieldLength)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ProfileValidationException(fieldName + " is empty.");
            }
            if (name.Length > fieldLength)
            {
                throw new ProfileValidationException(fieldName + " is too long!");
            }
        }

        private void ValidateDate(DateTime dateValue)
        {
            if (dateValue < Profile.MaxLengthFor.MinDate)
            {
                throw new ProfileValidationException("Date is too small!");
            }
        }
    }
}
