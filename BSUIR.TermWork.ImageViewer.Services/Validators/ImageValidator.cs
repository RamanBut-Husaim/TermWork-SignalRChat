// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The image validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The image validator.
    /// </summary>
    internal sealed class ImageValidator : EntityValidator<Image>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageValidator"/> class.
        /// </summary>
        public ImageValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Description, this.ValidateImageDescription);
            this.RegisterProperty(p => p.Name, this.ValidateImageName);
            this.RegisterProperty(p => p.Rate, this.ValidateRate);
            this.RegisterProperty(p => p.Extension, this.ValidateExtension);
            this.RegisterProperty(p => p.UploadDate, this.ValidateUploadDate);
            this.RegisterProperty(p => p.ContentType, this.ValidateContentType);
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
        public override void Validate(Image entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.ValidateKey(entity.Key);
            this.ValidateContentType(entity.ContentType);
            this.ValidateUploadDate(entity.UploadDate);
            this.ValidateExtension(entity.Extension);
            this.ValidateImageName(entity.Name);
            this.ValidateImageDescription(entity.Description);
            this.ValidateRate(entity.Rate);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The validate content type.
        /// </summary>
        /// <param name="contentType">
        /// The content type.
        /// </param>
        private void ValidateContentType(string contentType)
        {
            this.ValidateName(contentType, "Content Type", Image.MaxLengthFor.ContentType);
        }

        /// <summary>
        /// The validate extension.
        /// </summary>
        /// <param name="extension">
        /// The extension.
        /// </param>
        private void ValidateExtension(string extension)
        {
            this.ValidateName(extension, "Extension", Image.MaxLengthFor.Extension);
        }

        /// <summary>
        /// The validate image description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        private void ValidateImageDescription(string description)
        {
            this.ValidateName(description, "Description", Image.MaxLengthFor.Description);
        }

        /// <summary>
        /// The validate image name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        private void ValidateImageName(string name)
        {
            this.ValidateName(name, "Name", Image.MaxLengthFor.Name);
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
        /// <exception cref="ImageValidationException">
        /// </exception>
        private void ValidateName(string name, string fieldName, int fieldLength)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ImageValidationException(fieldName + " is empty.");
            }

            if (name.Length > fieldLength)
            {
                throw new ImageValidationException(fieldName + " is too long!");
            }
        }

        /// <summary>
        /// The validate rate.
        /// </summary>
        /// <param name="rate">
        /// The rate.
        /// </param>
        /// <exception cref="ImageValidationException">
        /// </exception>
        private void ValidateRate(int rate)
        {
            if (rate < Image.MaxLengthFor.RateMin || rate > Image.MaxLengthFor.RateMax)
            {
                throw new ImageValidationException("Rate is out of range.");
            }
        }

        /// <summary>
        /// The validate upload date.
        /// </summary>
        /// <param name="creationDate">
        /// The creation date.
        /// </param>
        /// <exception cref="ImageValidationException">
        /// </exception>
        private void ValidateUploadDate(DateTime creationDate)
        {
            if (creationDate <= Image.MaxLengthFor.MinSqlDateTime)
            {
                throw new ImageValidationException("Invalid creation date.");
            }
        }

        #endregion
    }
}