using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    internal sealed class MessageValidator : EntityValidator<Message>
    {
        #region Overrides of EntityValidator<Message>

        public MessageValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.SendDate, this.ValidateCreationDate);
            this.RegisterProperty(p => p.Text, this.ValidateText);
        }

        public override void Validate(Message entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.ValidateKey(entity.Key);
            this.ValidateText(entity.Text);
            this.ValidateCreationDate(entity.SendDate);
        }

        private void ValidateText(string text)
        {
            this.ValidateName(text, "Text", Message.MaxLengthFor.Text);
        }

        private void ValidateName(string name, string fieldName, int fieldLength)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new MessageValidationException(fieldName + " is empty.");
            }

            if (name.Length > fieldLength)
            {
                throw new MessageValidationException(fieldName + " is too long!");
            }
        }

        private void ValidateCreationDate(DateTime creationDate)
        {
            if (creationDate <= Message.MaxLengthFor.MinSqlDateTime)
            {
                throw new MessageValidationException("Invalid creation date.");
            }
        }

        #endregion
    }
}
