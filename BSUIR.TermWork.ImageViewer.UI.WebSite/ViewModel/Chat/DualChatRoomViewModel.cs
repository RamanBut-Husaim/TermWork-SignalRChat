using System.Collections.Generic;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Chat
{
    public sealed class DualChatRoomViewModel
    {
        public DualChatRoomViewModel()
        {
            this.Messages = new List<MessageViewModel>();
        }

        public string SenderFirstName { get; set; }

        public string SenderLastName { get; set; }

        public int? SenderUserKey { get; set; }

        public string ReceiverFirstName { get; set; }

        public string ReceiverLastName { get; set; }

        public int? ReceiverUserKey { get; set; }

        public IList<MessageViewModel> Messages { get; set; }
    }
}