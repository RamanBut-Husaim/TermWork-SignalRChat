using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Friendship;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public interface IFriendshipMapper
    {
        FriendshipRequestViewModel BuildViewModel(FriendshipRequest friendshipRequest);

        FriendViewModel Build(Profile profile);
    }
}