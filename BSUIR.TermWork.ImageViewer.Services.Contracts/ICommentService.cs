using System.Collections.Generic;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface ICommentService : IService
    {
        #region Public Methods and Operators

        void CreateComment(User owner, Image image, Comment comment);

        Comment GetCommentByKey(int key);

        IList<Comment> GetCommentsForImage(Image image);

        bool RemoveComment(Comment comment);

        #endregion
    }
}