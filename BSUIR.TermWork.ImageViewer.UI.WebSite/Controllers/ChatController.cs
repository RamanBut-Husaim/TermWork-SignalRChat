using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Chat;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [CustomAuthFilter]
    [RoutePrefix("user/{key:int}/chat")]
    public sealed class ChatController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IMembershipService _membershipService;
        private readonly IChatMapper _chatMapper;

        public ChatController(
            IMessageService messageService,
            IMembershipService membershipService,
            IChatMapper chatMapper)
        {
            this._messageService = messageService;
            this._membershipService = membershipService;
            this._chatMapper = chatMapper;
        }

        [HttpGet]
        [Route("getdaymessages")]
        [AjaxRequestOnlyFilter]

        public PartialViewResult GetDayMessages(int? friendKey)
        {
            var userIdentity = this.User.Identity as CustomIdentity;
            IList<MessageViewModel> result = new List<MessageViewModel>();

            if (friendKey.HasValue)
            {
                User currentUser = this._membershipService.GetUserByKey(userIdentity.Id);
                User friend = this._membershipService.GetUserByKey(friendKey.GetValueOrDefault());
                IEnumerable<Message> messages = this._messageService.GetDayChatMessages(
                    currentUser,
                    friend);
                result = messages.Select(p => this._chatMapper.MapMessage(p)).ToList();
            }

            return this.PartialView("_MessageList", result);
        }

        [HttpGet]
        [Route("dualchatroom")]
        [AjaxRequestOnlyFilter]
        public PartialViewResult DualChatRoom(int? friendKey)
        {
            var userIdentity = this.User.Identity as CustomIdentity;
            var chatViewModel = new DualChatRoomViewModel();

            if (friendKey.HasValue)
            {
                User currentUser = this._membershipService.GetUserByKey(userIdentity.Id);
                User friend = this._membershipService.GetUserByKey(friendKey.GetValueOrDefault());
                IEnumerable<Message> messages = this._messageService.GetDayChatMessages(
                    currentUser,
                    friend);
                chatViewModel = this._chatMapper.Map(currentUser, friend);
                chatViewModel.Messages =
                    messages.Select(p => this._chatMapper.MapMessage(p)).ToList();
            }

            return this.PartialView("_DualChatRoom", chatViewModel);
        }    

    }
}