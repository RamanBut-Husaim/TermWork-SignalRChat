using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal sealed class ImageValidator : EntityValidator<Image>
    {
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

        #region Overrides of EntityValidator<Image>

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

        private void ValidateImageName(string name)
        {
            this.ValidateName(name, "Name", Image.MaxLengthFor.Name);
        }

        private void ValidateExtension(string extension)
        {
            this.ValidateName(extension, "Extension", Image.MaxLengthFor.Extension);
        }

        private void ValidateContentType(string contentType)
        {
            this.ValidateName(contentType, "Content Type", Image.MaxLengthFor.ContentType);
        }

        private void ValidateRate(int rate)
        {
            if (rate < Image.MaxLengthFor.RateMin || rate > Image.MaxLengthFor.RateMax)
            {
                throw new ImageValidationException("Rate is out of range.");
            }
        }

        private void ValidateImageDescription(string description)
        {
            this.ValidateName(description, "Description", Image.MaxLengthFor.Description);
        }

        private void ValidateUploadDate(DateTime creationDate)
        {
            if (creationDate <= Image.MaxLengthFor.MinSqlDateTime)
            {
                throw new ImageValidationException("Invalid creation date.");
            }
        }

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

        #endregion
    }
}
