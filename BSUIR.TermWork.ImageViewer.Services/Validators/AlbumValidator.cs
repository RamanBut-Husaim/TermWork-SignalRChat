using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal sealed class AlbumValidator : EntityValidator<Album>
    {
        #region Overrides of EntityValidator<Album>

        public AlbumValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Name, this.ValidateAlbumName);
            this.RegisterProperty(p => p.Description, this.ValidateDescription);
            this.RegisterProperty(p => p.ImageNumber, this.ValidateImageNumber);
            this.RegisterProperty(p => p.CreationDate, this.ValidateCreationDate);
        }

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

        private void ValidateAlbumName(string name)
        {
            this.ValidateName(name, "Name", Album.MaxLengthFor.Name);
        }

        private void ValidateDescription(string description)
        {
            this.ValidateName(description, "Description", Album.MaxLengthFor.Description);
        }

        private void ValidateImageNumber(int imageCount)
        {
            if (imageCount < 0)
            {
                throw new AlbumValidationException("Image count cannot be below zero.");
            }
        }

        private void ValidateCreationDate(DateTime creationDate)
        {
            if (creationDate <= Album.MaxLengthFor.MinSqlDateTime)
            {
                throw new AlbumValidationException("Invalid creation date.");
            }
        }

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
