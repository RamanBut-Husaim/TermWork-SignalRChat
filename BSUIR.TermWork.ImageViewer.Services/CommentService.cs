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
    public sealed class CommentService : ServiceBase, ICommentService
    {
        private readonly IEntityValidator<Comment> _commentValidator;
        private readonly IEntityValidator<Image> _imageValidator;
        private readonly IEntityValidator<User> _userValidator;

        public CommentService(
            IUnitOfWork unitOfWork,
            IEntityValidator<Comment> commentValidator,
            IEntityValidator<Image> imageValidator,
            IEntityValidator<User> userValidator)
            : base(unitOfWork)
        {
            this._commentValidator = commentValidator;
            this._imageValidator = imageValidator;
            this._userValidator = userValidator;
        }

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
    }
}
