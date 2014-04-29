using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Notification;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [CustomAuthFilter]
    [RoutePrefix("user/{key:int}/notifications")]
    public sealed class NotificationController : BaseController
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IMembershipService _membershipService;

        public NotificationController(
            IFriendshipService friendshipService,
            IMembershipService membershipService)
        {
            this._friendshipService = friendshipService;
            this._membershipService = membershipService;
        }

        [HttpGet]
        [Route("notificationlist")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult NotificationList()
        {
            var model = new NotificationCountViewModel();
            var user = this.User.Identity as CustomIdentity;

            var sender = this._membershipService.GetUserByKey(user.Id);
            model.Count = this._friendshipService.GetUnconfirmedRequests(sender).Count();

            return this.PartialView("_Notifications", model);
        }
    }
}