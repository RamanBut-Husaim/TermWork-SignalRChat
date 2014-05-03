using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Validators;

namespace BSUIR.TermWork.ImageViewer.Services
{
    public sealed class MessageService : ServiceBase, IMessageService
    {
        private readonly IEntityValidator<Message> _messageValidator;
        private readonly IEntityValidator<User> _userValidator; 

        public MessageService(
            IUnitOfWork unitOfWork,
            IEntityValidator<Message> messageValidator,
            IEntityValidator<User> userValidator) : base(unitOfWork)
        {
            this._messageValidator = messageValidator;
            this._userValidator = userValidator;
        }

        public async Task SendMessageAsync(Message message)
        {
            this._messageValidator.Validate(message);

            try
            {
                Friendship friendship = this.GetFriendship(
                    message.Sender.User,
                    message.Receiver.User);
                message.Friendship = friendship;
                this.UnitOfWork.Repository<Message, int>().Insert(message);
                await this.UnitOfWork.SaveAsync();
            }
            catch (DbException ex)
            {
                throw new MessageServiceException(EntityValidationException.DefaultMessage, ex);
            }
        }

        public IEnumerable<Message> GetDayChatMessages(User firstUser, User secondUser)
        {
            DateTime earlyDate = DateTime.Now.AddDays(-1);
            return this.GetChatMessagesInternal(firstUser, secondUser, earlyDate);
        }

        public IEnumerable<Message> GetWeekChatMessages(User firstUser, User secondUser)
        {
            DateTime earlyDate = DateTime.Now.AddDays(-7);
            return this.GetChatMessagesInternal(firstUser, secondUser, earlyDate);
        }

        public IEnumerable<Message> GetMonthChatMessages(User firstUser, User secondUser)
        {
            DateTime earlyDate = DateTime.Now.AddMonths(-1);
            return this.GetChatMessagesInternal(firstUser, secondUser, earlyDate);
        }  

        public IEnumerable<Message> GetChatMessages(User firstUser, User secondUser)
        {
            this._userValidator.Validate(firstUser);
            this._userValidator.Validate(secondUser);
            IEnumerable<Message> result;
            try
            {
                Friendship friendship = this.GetFriendship(firstUser, secondUser);
                result =
                    this.UnitOfWork.Repository<Message, int>()
                        .Query()
                        .Filter(p => p.Friendship.Key.Equals(friendship.Key))
                        .OrderBy(p => p.OrderBy(f => f.SendDate))
                        .Get();
            }
            catch (DbException ex)
            {
                throw new MessageServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        }

        private IEnumerable<Message> GetChatMessagesInternal(
            User firstUser,
            User secondUser,
            DateTime fromDate)
        {
            this._userValidator.Validate(firstUser);
            this._userValidator.Validate(secondUser);
            IEnumerable<Message> result;
            try
            {
                Friendship friendship = this.GetFriendship(firstUser, secondUser);
                result =
                    this.UnitOfWork.Repository<Message, int>()
                        .Query()
                        .Filter(
                            p => p.Friendship.Key.Equals(friendship.Key) && p.SendDate > fromDate)
                        .OrderBy(p => p.OrderBy(f => f.SendDate))
                        .Get();
            }
            catch (DbException ex)
            {
                throw new MessageServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result;
        } 

        private Friendship GetFriendship(User firstUser, User secondUser)
        {
            return
                this.UnitOfWork.Repository<Friendship, int>()
                    .Query()
                    .Filter(
                        p =>
                        (p.FirstProfile.Key == firstUser.UserProfile.Key
                         || p.FirstProfile.Key == secondUser.UserProfile.Key)
                        && (p.SecondProfile.Key == firstUser.UserProfile.Key
                            || p.SecondProfile.Key == secondUser.UserProfile.Key))
                    .Include(p => p.FirstProfile)
                    .Include(p => p.SecondProfile)
                    .Get()
                    .FirstOrDefault();
        }
    }
}
