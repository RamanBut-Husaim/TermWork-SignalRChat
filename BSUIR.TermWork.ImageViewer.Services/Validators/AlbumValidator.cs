// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AlbumValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The album validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The album validator.
    /// </summary>
    internal sealed class AlbumValidator : EntityValidator<Album>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumValidator"/> class.
        /// </summary>
        public AlbumValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Name, this.ValidateAlbumName);
            this.RegisterProperty(p => p.Description, this.ValidateDescription);
            this.RegisterProperty(p => p.ImageNumber, this.ValidateImageNumber);
            this.RegisterProperty(p => p.CreationDate, this.ValidateCreationDate);
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
        public override void Validate(Album entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.ValidateKey(entity.Key);
            this.ValidateAlbumName(entity.Name);
            this.ValidateCreationDate(entity.CreationDate);
            this.ValidateImageNumber(entity.ImageNumber);
            this.ValidateDescription(entity.Description);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The validate album name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        private void ValidateAlbumName(string name)
        {
            this.ValidateName(name, "Name", Album.MaxLengthFor.Name);
        }

        /// <summary>
        /// The validate creation date.
        /// </summary>
        /// <param name="creationDate">
        /// The creation date.
        /// </param>
        /// <exception cref="AlbumValidationException">
        /// </exception>
        private void ValidateCreationDate(DateTime creationDate)
        {
            if (creationDate <= Album.MaxLengthFor.MinSqlDateTime)
            {
                throw new AlbumValidationException("Invalid creation date.");
            }
        }

        /// <summary>
        /// The validate description.
        /// </summary>
        /// <param name="description">
        /// The description.
        /// </param>
        private void ValidateDescription(string description)
        {
            this.ValidateName(description, "Description", Album.MaxLengthFor.Description);
        }

        /// <summary>
        /// The validate image number.
        /// </summary>
        /// <param name="imageCount">
        /// The image count.
        /// </param>
        /// <exception cref="AlbumValidationException">
        /// </exception>
        private void ValidateImageNumber(int imageCount)
        {
            if (imageCount < 0)
            {
                throw new AlbumValidationException("Image count cannot be below zero.");
            }
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
        /// <exception cref="AlbumValidationException">
        /// </exception>
        private void ValidateName(string name, string fieldName, int fieldLength)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new AlbumValidationException(fieldName + " is empty.");
            }

            if (name.Length > fieldLength)
            {
                throw new AlbumValidationException(fieldName + " is too long!");
            }
        }

        #endregion
    }
}