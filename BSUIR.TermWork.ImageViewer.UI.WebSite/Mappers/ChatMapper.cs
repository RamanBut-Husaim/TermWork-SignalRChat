using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Chat;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public sealed class ChatMapper : IChatMapper
    {
        public DualChatRoomViewModel Map(User sender, User receiver)
        {
            var result = new DualChatRoomViewModel();

            result.SenderFirstName = sender.UserProfile.FirstName;
            result.SenderLastName = sender.UserProfile.LastName;
            result.SenderUserKey = sender.Key;

            result.ReceiverFirstName = receiver.UserProfile.FirstName;
            result.ReceiverLastName = receiver.UserProfile.LastName;
            result.ReceiverUserKey = receiver.Key;

            return result;
        }

        public MessageViewModel MapMessage(Message message)
        {
            var result = new MessageViewModel();

            result.Author = string.Format(
                "{0} {1}",
                message.Sender.FirstName,
                message.Sender.LastName);
            result.Date = message.SendDate;
            result.Text = message.Text;

            return result;
        }
    }
}