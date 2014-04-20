using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal sealed class CommentValidator : EntityValidator<Comment>
    {
        #region Overrides of EntityValidator<Comment>

        public CommentValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Content, this.ValidateContent);
            this.RegisterProperty(p => p.CreationDate, this.ValidateCreationDate);
            this.RegisterProperty(p => p.Rate, this.ValidateRate);
        }

        public override void Validate(Comment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.ValidateContent(entity.Content);
            this.ValidateCreationDate(entity.CreationDate);
            this.ValidateRate(entity.Rate);
            this.ValidateKey(entity.Key);
        }

        private void ValidateContent(string content)
        {
            this.ValidateName(content, "Content", Comment.MaxLengthFor.Content);
        }

        private void ValidateCreationDate(DateTime creationDate)
        {
            if (creationDate <= Comment.MaxLengthFor.MinSqlDateTime)
            {
                throw new CommentValidationException("Invalid creation date.");
            }
        }

        private void ValidateRate(int rate)
        {
            if (rate < Comment.MaxLengthFor.RateMin || rate > Comment.MaxLengthFor.RateMax)
            {
                throw new CommentValidationException("Rate is out of range.");
            }
        }

        private void ValidateName(string name, string fieldName, int fieldLength)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new CommentValidationException(fieldName + " is empty.");
            }
            if (name.Length > fieldLength)
            {
                throw new CommentValidationException(fieldName + " is too long!");
            }
        }

        #endregion
    }
}
