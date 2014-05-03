using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Chat;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public interface IChatMapper
    {
        DualChatRoomViewModel Map(User sender, User receiver);

        MessageViewModel MapMessage(Message message);
    }
}
