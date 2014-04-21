using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface IFriendshipService : IService
    {
        void SendRequest(User sender, User receiver);

        void ConfirmRequest(User sender, User receiver);

        void DeclineRequest(User sender, User receiver);

        void RemoveFriend(User sender, User receiver);
    }
}
