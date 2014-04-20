using System;
using System.Text.RegularExpressions;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal sealed class UserValidator : EntityValidator<User>
    {
        #region Overrides of EntityValidator<User>

        public UserValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Email, this.ValidateEmail);
            this.RegisterProperty(p => p.PasswordHash, this.ValidatePassword);
        }

        public override void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            this.ValidateKey(user.Key);
            this.ValidateEmail(user.Email);
            this.ValidatePassword(user.PasswordHash);
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new UserValidationException("The email property is null or empty.");
            }
            var emailValidation = new Regex(
                User.MaxLengthFor.EmailValidation,
                RegexOptions.IgnoreCase);
            if (!emailValidation.IsMatch(email))
            {
                throw new UserValidationException(
                    "The email property is incorrect. It does not satisfy the pattern "
                    + User.MaxLengthFor.EmailValidation);
            }
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new UserValidationException(
                    "The password property is null or empty. It must be at least "
                    + User.MaxLengthFor.PasswordMinimum + " characters long.");
            }

            if (password.Length < User.MaxLengthFor.PasswordMinimum
                || password.Length > User.MaxLengthFor.PasswordHash)
            {
                throw new UserValidationException(
                    "The password property has incorrect length. It should be between "
                    + User.MaxLengthFor.PasswordMinimum + " and " + User.MaxLengthFor.PasswordHash
                    + " charcaters long.");
            }
        }

        #endregion
    }
}
