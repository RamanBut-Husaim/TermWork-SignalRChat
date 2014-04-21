// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentValidator.cs" company="">
//   
// </copyright>
// <summary>
//   The comment validator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;

namespace BSUIR.TermWork.ImageViewer.Services.Validators
{
    /// <summary>
    /// The comment validator.
    /// </summary>
    internal sealed class CommentValidator : EntityValidator<Comment>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentValidator"/> class.
        /// </summary>
        public CommentValidator()
        {
            this.RegisterProperty(p => p.Key, this.ValidateKey);
            this.RegisterProperty(p => p.Content, this.ValidateContent);
            this.RegisterProperty(p => p.CreationDate, this.ValidateCreationDate);
            this.RegisterProperty(p => p.Rate, this.ValidateRate);
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

        #endregion

        #region Methods

        /// <summary>
        /// The validate content.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        private void ValidateContent(string content)
        {
            this.ValidateName(content, "Content", Comment.MaxLengthFor.Content);
        }

        /// <summary>
        /// The validate creation date.
        /// </summary>
        /// <param name="creationDate">
        /// The creation date.
        /// </param>
        /// <exception cref="CommentValidationException">
        /// </exception>
        private void ValidateCreationDate(DateTime creationDate)
        {
            if (creationDate <= Comment.MaxLengthFor.MinSqlDateTime)
            {
                throw new CommentValidationException("Invalid creation date.");
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
        /// <exception cref="CommentValidationException">
        /// </exception>
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

        /// <summary>
        /// The validate rate.
        /// </summary>
        /// <param name="rate">
        /// The rate.
        /// </param>
        /// <exception cref="CommentValidationException">
        /// </exception>
        private void ValidateRate(int rate)
        {
            if (rate < Comment.MaxLengthFor.RateMin || rate > Comment.MaxLengthFor.RateMax)
            {
                throw new CommentValidationException("Rate is out of range.");
            }
        }

        #endregion
    }
}