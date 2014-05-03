using System.Collections.Generic;
using System.Threading.Tasks;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface IMessageService : IService
    {
        Task SendMessageAsync(Message message);

        IEnumerable<Message> GetChatMessages(User firstUser, User secondUser);

        IEnumerable<Message> GetWeekChatMessages(User firstUser, User secondUser);

        IEnumerable<Message> GetDayChatMessages(User firstUser, User secondUser);

        IEnumerable<Message> GetMonthChatMessages(User firstUser, User secondUser);
    }
}
