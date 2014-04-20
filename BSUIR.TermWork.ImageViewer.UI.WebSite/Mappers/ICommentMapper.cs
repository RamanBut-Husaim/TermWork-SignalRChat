using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Comment;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public interface ICommentMapper
    {
        CommentListItemViewModel BuildListItem(Comment comment);

        Comment BuildComment(CommentCreateViewModel viewModel);

        CommentCreateViewModel BuildCreate(Comment comment);
    }
}