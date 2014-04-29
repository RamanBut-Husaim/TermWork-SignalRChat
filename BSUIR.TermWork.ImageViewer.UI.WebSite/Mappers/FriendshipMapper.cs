using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Friendship;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public sealed class FriendshipMapper : IFriendshipMapper
    {
        #region Implementation of IFriendshipMapper

        public FriendshipRequestViewModel BuildViewModel(FriendshipRequest friendshipRequest)
        {
            var result = new FriendshipRequestViewModel();

            result.UserKey = friendshipRequest.Sender.User.Key;
            result.FirstName = friendshipRequest.Sender.FirstName;
            result.LastName = friendshipRequest.Sender.LastName;
            result.Email = friendshipRequest.Sender.User.Email;
            result.RequestDate = friendshipRequest.CreationdDate;

            return result;
        }

        public FriendViewModel Build(Profile profile)
        {
            var result = new FriendViewModel();

            result.Key = profile.User.Key;
            result.FirstName = profile.FirstName;
            result.LastName = profile.LastName;

            return result;
        }

        #endregion
    }
}