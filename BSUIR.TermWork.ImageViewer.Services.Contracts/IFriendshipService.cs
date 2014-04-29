using System.Collections;
using System.Collections.Generic;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface IFriendshipService : IService
    {
        void SendRequest(User sender, User receiver);

        void ConfirmRequest(User sender, User receiver);

        void DeclineRequest(User sender, User receiver);

        void RemoveFriend(User firstFriend, User secondFriend);

        IEnumerable<FriendshipRequest> GetUnconfirmedRequests(User receiver);

        IEnumerable<Profile> GetFriends(User user);

        IEnumerable<Profile> GetFriends(int userKey);
    }
}
