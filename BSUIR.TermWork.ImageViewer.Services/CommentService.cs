// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentService.cs" company="">
//   
// </copyright>
// <summary>
//   The comment service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections.Generic;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Data.Repositories;
using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;

namespace BSUIR.TermWork.ImageViewer.Services
{
    /// <summary>
    /// The comment service.
    /// </summary>
    public sealed class CommentService : ServiceBase, ICommentService
    {
        #region Fields

        /// <summary>
        /// The _comment validator.
        /// </summary>
        private readonly IEntityValidator<Comment> _commentValidator;

        /// <summary>
        /// The _image validator.
        /// </summary>
        private readonly IEntityValidator<Image> _imageValidator;

        /// <summary>
        /// The _user validator.
        /// </summary>
        private readonly IEntityValidator<User> _userValidator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="commentValidator">
        /// The comment validator.
        /// </param>
        /// <param name="imageValidator">
        /// The image validator.
        /// </param>
        /// <param name="userValidator">
        /// The user validator.
        /// </param>
        public CommentService(
            IUnitOfWork unitOfWork, 
            IEntityValidator<Comment> commentValidator, 
            IEntityValidator<Image> imageValidator, 
            IEntityValidator<User> userValidator) : base(unitOfWork)
        {
            this._commentValidator = commentValidator;
            this._imageValidator = imageValidator;
            this._userValidator = userValidator;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create comment.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <exception cref="CommentServiceException">
        /// </exception>
        public void CreateComment(User owner, Image image, Comment comment)
        {
            this._userValidator.Validate(owner);
            this._imageValidator.Validate(image);
            this._commentValidator.Validate(comment);
            try
            {
                User tempUser = this.UnitOfWork.Repository<User, int>().FindByKey(owner.Key);
                Image tempImage = this.UnitOfWork.Repository<Image, int>().FindByKey(image.Key);
                if (tempUser != null && tempImage != null)
                {
                    comment.Owner = tempUser;
                    comment.Image = tempImage;
                    this.UnitOfWork.Repository<Comment, int>().Insert(comment);
                    this.UnitOfWork.Save();
                }
            }
            catch (DbException ex)
            {
                throw new CommentServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        /// <summary>
        /// The get comment by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Comment"/>.
        /// </returns>
        /// <exception cref="CommentServiceException">
        /// </exception>
        public Comment GetCommentByKey(int key)
        {
            this._commentValidator.ValidateProperty(p => p.Key, key);
            Comment result;
            try
            {
                result = this.UnitOfWork.Repository<Comment, int>().FindByKey(key);
            }
            catch (DbException ex)
            {
                throw new CommentServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The get comments for image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="CommentServiceException">
        /// </exception>
        public IList<Comment> GetCommentsForImage(Image image)
        {
            this._imageValidator.Validate(image);
            IList<Comment> result;
            try
            {
                result =
                    this.UnitOfWork.Repository<Comment, int>()
                        .Query()
                        .Include(p => p.Image)
                        .Filter(p => p.Image.Key.Equals(image.Key))
                        .Get()
                        .ToList();
            }
            catch (DbException ex)
            {
                throw new CommentServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        /// <summary>
        /// The remove comment.
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="CommentServiceException">
        /// </exception>
        public bool RemoveComment(Comment comment)
        {
            bool result = false;
            this._commentValidator.Validate(comment);
            try
            {
                IRepository<Comment, int> commentRepository =
                    this.UnitOfWork.Repository<Comment, int>();
                Comment commentToRemove = commentRepository.FindByKey(comment.Key);
                if (commentToRemove != null)
                {
                    commentRepository.Delete(commentToRemove);
                    result = true;
                }

                this.UnitOfWork.Save();
            }
            catch (DbException ex)
            {
                throw new CommentServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        #endregion
    }
}