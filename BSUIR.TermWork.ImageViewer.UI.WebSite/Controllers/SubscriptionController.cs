using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Subscription;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [RoutePrefix("user/{key:int}/subscriptions")]
    [CustomAuthFilter]
    public class SubscriptionController : BaseController
    {
        private readonly IMembershipService _membershipService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionMapper _subscriptionMapper;

        public SubscriptionController(
            IMembershipService membershipService,
            ISubscriptionService subscriptionService,
            ISubscriptionMapper subscriptionMapper)
        {
            this._membershipService = membershipService;
            this._subscriptionService = subscriptionService;
            this._subscriptionMapper = subscriptionMapper;
        }

        [HttpGet]
        [Route("suscribeoperation")]
        [ViewBagPopulatorFilter]
        [SubscriptionVerificationFilter]
        [ChildActionOnly]
        public PartialViewResult SubscribeOperation()
        {
            return this.PartialView("_SubscribeOperation");
        }

        [HttpGet]
        [Route("subscribe")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public PartialViewResult Subscribe(int? key)
        {
            SubscriptionCreateViewModel result = null;
            if (key.HasValue)
            {
                result = new SubscriptionCreateViewModel();
                result.UserKey = key.Value;
            }
            else
            {
                this.TempData[Constants.TempDataErrorMessage] = "User is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_Subscribe", result);
        }

        [HttpPost]
        [Route("subscribe")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public ActionResult Subscribe(SubscriptionCreateViewModel viewModel)
        {
            var user = User.Identity as CustomIdentity;
            bool result = false;

            if (viewModel != null)
            {
                try
                {
                    User subscriber = this._membershipService.GetUserByKey(user.Id);
                    User target = this._membershipService.GetUserByKey(viewModel.Key);
                    this._subscriptionService.AddSubscription(subscriber, target);
                    result = true;
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }

            if (result)
            {
                return this.Json(new { success = result });
            }

            return this.PartialView("_Success");
        }

        [HttpGet]
        [Route("unsubscribe")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public PartialViewResult Unsubscribe(int? key)
        {
            SubscriptionCreateViewModel result = null;
            if (key.HasValue)
            {
                result = new SubscriptionCreateViewModel();
                result.UserKey = key.Value;
            }
            else
            {
                this.TempData[Constants.TempDataErrorMessage] = "User is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_Unsubscribe", result);
        }

        [HttpPost]
        [Route("unsubscribe")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public ActionResult Unsubscribe(SubscriptionCreateViewModel viewModel)
        {
            var user = User.Identity as CustomIdentity;
            bool result = false;

            if (viewModel != null)
            {
                try
                {
                    User subscriber = this._membershipService.GetUserByKey(user.Id);
                    User target = this._membershipService.GetUserByKey(viewModel.Key);
                    this._subscriptionService.RemoveSubscription(subscriber, target);
                    result = true;
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }

            if (result)
            {
                return this.Json(new { success = result });
            }

            return this.PartialView("_Success");
        }

        [HttpGet]
        [Route("subscribers")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult Subscribers(int? key)
        {
            var user = User.Identity as CustomIdentity;
            IList<SubscriptionTargetViewModel> result = new List<SubscriptionTargetViewModel>();

            try
            {
                IList<Subscription> subscriptions =
                    this._subscriptionService.GetFilteredSubscriptionsForUser(user.Id);
                User subscriber = this._membershipService.GetUserByKey(user.Id);
                for (int i = 0; i < subscriptions.Count; ++i)
                {
                    int albumCount = this._subscriptionService.CalculateNumberOfNewAlbums(
                        subscriber,
                        subscriptions[i].Target.User);
                    int imageCount = this._subscriptionService.CalculateNumberOfNewImages(
                        subscriber,
                        subscriptions[i].Target.User);
                    SubscriptionTargetViewModel viewModel =
                        this._subscriptionMapper.BuildTarget(
                            subscriptions[i],
                            albumCount + imageCount);
                    result.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_Subscribers", result);
        }
    }
}