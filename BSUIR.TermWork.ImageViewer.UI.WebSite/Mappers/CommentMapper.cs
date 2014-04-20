using System;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Comment;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public class CommentMapper : ICommentMapper
    {
        public CommentListItemViewModel BuildListItem(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            var result = new CommentListItemViewModel();
            result.Key = comment.Key;
            result.Content = comment.Content;
            result.CreationDate = comment.CreationDate;
            result.UserName = string.Format(
                "{0} {1}",
                comment.Owner.UserProfile.FirstName,
                comment.Owner.UserProfile.LastName);
            return result;
        }

        public Comment BuildComment(CommentCreateViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            var result = new Comment();
            result.Rate = 0;
            result.CreationDate = DateTime.UtcNow;
            result.Content = viewModel.Content;
            return result;
        }

        public CommentCreateViewModel BuildCreate(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment");
            }

            var result = new CommentCreateViewModel();
            result.Key = comment.Key;
            result.CreationDate = comment.CreationDate;
            result.Content = comment.Content;
            return result;
        }
    }
}