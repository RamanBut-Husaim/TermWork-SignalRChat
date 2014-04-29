using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Friendship;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [CustomAuthFilter]
    [RoutePrefix("user/{key:int}/friends")]
    public sealed class FriendshipController : BaseController
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IMembershipService _membershipService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IFriendshipMapper _friendshipMapper;

        public FriendshipController(
            IFriendshipService friendshipService,
            IMembershipService membershipService,
            ISubscriptionService subscriptionService,
            IFriendshipMapper friendshipMapper)
        {
            this._friendshipService = friendshipService;
            this._membershipService = membershipService;
            this._subscriptionService = subscriptionService;
            this._friendshipMapper = friendshipMapper;
        }

        [HttpGet]
        [Route("removefriend")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public PartialViewResult RemoveFriend(int? key)
        {
            FriendRequestCreateViewModel model;
            if (key.HasValue)
            {
                model = new FriendRequestCreateViewModel();
                model.Key = key.Value;
            }
            else
            {
                this.TempData[Constants.TempDataErrorMessage] = "User is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_RemoveFriend", model);
        }

        [HttpPost]
        [Route("removefriend")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public ActionResult RemoveFriend(FriendRequestCreateViewModel viewModel)
        {
            var user = User.Identity as CustomIdentity;

            if (viewModel != null)
            {
                try
                {
                    User firstFriend = this._membershipService.GetUserByKey(user.Id);
                    User secondFriend =
                        this._membershipService.GetUserByKey(viewModel.Key.GetValueOrDefault());
                    this._friendshipService.RemoveFriend(firstFriend, secondFriend);
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }

            return this.Json(new { success = true });
        }

        [HttpGet]
        [Route("sendfriendshiprequest")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public PartialViewResult SendFriendshipRequest(int? key)
        {
            FriendRequestCreateViewModel model;
            if (key.HasValue)
            {
                model = new FriendRequestCreateViewModel();
                model.Key = key.Value;
            }
            else
            {
                this.TempData[Constants.TempDataErrorMessage] = "User is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_FriendshipRequest", model);
        }

        [HttpPost]
        [Route("sendfriendshiprequest")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public ActionResult SendFriendshipRequest(FriendRequestCreateViewModel viewModel)
        {
            bool result = true;
            var user = User.Identity as CustomIdentity;

            if (viewModel != null)
            {
                try
                {
                    User sender = this._membershipService.GetUserByKey(user.Id);
                    User receiver =
                        this._membershipService.GetUserByKey(viewModel.Key.GetValueOrDefault());
                    this._friendshipService.SendRequest(sender, receiver);
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }

            }

            if (result)
            {
                return this.Json(new { success = true });
            }

            return this.PartialView("_Success");
        }

        [HttpGet]
        [Route("friendshiprequestlist")]
        [ChildActionOnly]
        [ViewBagPopulatorFilter]
        public PartialViewResult FriendshipRequestList(int? key)
        {
            var user = User.Identity as CustomIdentity;
            IList<FriendshipRequestViewModel> result = new List<FriendshipRequestViewModel>();

            User receiver = this._membershipService.GetUserByKey(user.Id);
            if (receiver != null)
            {
                result =
                    this._friendshipService.GetUnconfirmedRequests(receiver)
                        .Select(p => this._friendshipMapper.BuildViewModel(p))
                        .ToList();
            }

            return this.PartialView("_FriendshipRequestList", result);
        }

        [HttpGet]
        [Route("confirmrequest")]
        [AjaxRequestOnlyFilter]
        [ViewBagPopulatorFilter]
        public PartialViewResult ConfirmRequest(int? senderKey)
        {
            var user = this.User.Identity as CustomIdentity;
            var answerViewModel = new FriendshipAnswerViewModel();
            answerViewModel.ReceiverKey = user.Id;
            answerViewModel.SenderKey = senderKey;

            return this.PartialView("_ConfirmFriendshipRequest", answerViewModel);
        }

        [HttpPost]
        [Route("confirmrequest")]
        [AjaxRequestOnlyFilter]
        [ViewBagPopulatorFilter]
        public ActionResult ConfirmRequest(FriendshipAnswerViewModel answerViewModel)
        {
            var user = this.User.Identity as CustomIdentity;

            if (answerViewModel != null)
            {
                try
                {
                    User receiver = this._membershipService.GetUserByKey(user.Id);
                    User sender =
                        this._membershipService.GetUserByKey(
                            answerViewModel.SenderKey.GetValueOrDefault());
                    this._friendshipService.ConfirmRequest(sender, receiver);
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }
            
            return this.Json(new { success = true });
        }

        [HttpGet]
        [Route("declinerequest")]
        [AjaxRequestOnlyFilter]
        [ViewBagPopulatorFilter]
        public PartialViewResult DeclineRequest(int? senderKey)
        {
            var user = this.User.Identity as CustomIdentity;
            var answerViewModel = new FriendshipAnswerViewModel();
            answerViewModel.ReceiverKey = user.Id;
            answerViewModel.SenderKey = senderKey;

            return this.PartialView("_DeclineFriendshipRequest", answerViewModel);
        }

        [HttpGet]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        [Route("friendslistnavbar")]
        public PartialViewResult FriendsListNavbar()
        {
            var userIdentity = this.User.Identity as CustomIdentity;
            User user = this._membershipService.GetUserByKey(userIdentity.Id);
            IList<FriendViewModel> friends =
                this._friendshipService.GetFriends(user)
                    .Select(p => this._friendshipMapper.Build(p))
                    .ToList();

            return this.PartialView("_FriendsListNavbar", friends);
        }

        [HttpPost]
        [Route("declinerequest")]
        [AjaxRequestOnlyFilter]
        [ViewBagPopulatorFilter]
        public ActionResult DeclineRequest(FriendshipAnswerViewModel answerViewModel)
        {
            var user = this.User.Identity as CustomIdentity;

            if (answerViewModel != null)
            {
                try
                {
                    User receiver = this._membershipService.GetUserByKey(user.Id);
                    User sender =
                        this._membershipService.GetUserByKey(
                            answerViewModel.SenderKey.GetValueOrDefault());
                    this._friendshipService.DeclineRequest(sender, receiver);
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }

            return this.Json(new { success = true });
        }

        [HttpGet]
        [Route("navigationbar")]
        [ChildActionOnly]
        [ViewBagPopulatorFilter]
        public PartialViewResult NavigationBar()
        {
            return this.PartialView("_FriendshipNavbar");
        }

        [HttpGet]
        [Route("index")]
        [ViewBagPopulatorFilter]
        public ActionResult Index(int? key)
        {
            this.UpdateSubscriptions(this._subscriptionService);
            this.UpdateFriendsList(this._friendshipService);
            this.ReleaseUpToDateSubscriptions(this._membershipService, this._subscriptionService);
            try
            {
                if (!(key.HasValue && this._membershipService.GetUserByKey(key.Value) != null))
                {
                    return this.RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View("FriendshipIndex");
        }
    }
}