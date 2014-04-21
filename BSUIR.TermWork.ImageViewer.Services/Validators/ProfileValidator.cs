// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The profile validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The profile validator.
    /// </summary>
    internal sealed class ProfileValidator : EntityValidator<Profile>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileValidator"/> class.
        /// </summary>
        public ProfileValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.FirstName, this.ValidateFirstName);
            this.RegisterProperty(p => p.LastName, this.ValidateLastName);
            this.RegisterProperty(p => p.LastSignIn, this.ValidateDate);
            this.RegisterProperty(p => p.LastSignOut, this.ValidateDate);
            this.RegisterProperty(p => p.RegistrationDate, this.ValidateDate);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="profile">
        /// The profile.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
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

        #region Methods

        /// <summary>
        /// The validate date.
        /// </summary>
        /// <param name="dateValue">
        /// The date value.
        /// </param>
        /// <exception cref="ProfileValidationException">
        /// </exception>
        private void ValidateDate(DateTime dateValue)
        {
            if (dateValue < Profile.MaxLengthFor.MinDate)
            {
                throw new ProfileValidationException("Date is too small!");
            }
        }

        /// <summary>
        /// The validate first name.
        /// </summary>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        private void ValidateFirstName(string firstName)
        {
            this.ValidateName(firstName, "First name", Profile.MaxLengthFor.FirstName);
        }

        /// <summary>
        /// The validate last name.
        /// </summary>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        private void ValidateLastName(string lastName)
        {
            this.ValidateName(lastName, "Last name", Profile.MaxLengthFor.LastName);
        }

        /// <summary>
        /// The validate name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="fieldName">
        /// The field name.
        /// </param>
        /// <param name="fieldLength">
        /// The field length.
        /// </param>
        /// <exception cref="ProfileValidationException">
        /// </exception>
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

        #endregion
    }
}