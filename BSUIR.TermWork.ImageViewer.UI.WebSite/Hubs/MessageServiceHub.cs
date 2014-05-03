using System;
using System.Threading.Tasks;

using Autofac;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Chat;

using Microsoft.AspNet.SignalR;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Hubs
{
    public sealed class MessageServiceHub : Hub
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IMessageService _messageService;
        private readonly IMembershipService _membershipService;

        public MessageServiceHub(ILifetimeScope lifetimeScope) : base()
        {
            this._lifetimeScope = lifetimeScope.BeginLifetimeScope("AutofacWebRequest");
            this._messageService = this._lifetimeScope.Resolve<IMessageService>();
            this._membershipService = this._lifetimeScope.Resolve<IMembershipService>();
        }

        public async Task SendMessage(MessageViewModel messageViewModel)
        {
            var userIdentity = this.Context.User.Identity as CustomIdentity;
            int receiverKey = 0;
            if (userIdentity != null && int.TryParse(messageViewModel.Author, out receiverKey))
            {
                User sender = this._membershipService.GetUserByKey(userIdentity.Id);
                User receiver = this._membershipService.GetUserByKey(receiverKey);
                var message = new Message(
                    messageViewModel.Text,
                    sender.UserProfile,
                    receiver.UserProfile);
                message.SendDate = messageViewModel.Date ?? DateTime.Now;
                messageViewModel.Sender = string.Format(
                    "{0} {1}",
                    sender.UserProfile.FirstName,
                    sender.UserProfile.LastName);
                await this._messageService.SendMessageAsync(message);
                await this.Clients.User(messageViewModel.Author).ShowMessage(messageViewModel);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this._lifetimeScope != null)
            {
                this._lifetimeScope.Dispose();    
            }

            base.Dispose(disposing);
        }
    }
}