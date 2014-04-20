using System;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [RoutePrefix("user/{key:int}")]
    [CustomAuthFilter]
    public class PersonalPageController : BaseController
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMembershipService _membershipService;

        public PersonalPageController(
            ISubscriptionService subscriptionService,
            IMembershipService membershipService)
        {
            this._subscriptionService = subscriptionService;
            this._membershipService = membershipService;
        }

        [HttpGet]
        [ViewBagPopulatorFilter]
        [Route("index")]
        public ActionResult Index(int? key)
        {
            this.UpdateSubscriptions(this._subscriptionService);
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

            return this.View();
        }

        [HttpGet]
        [Route("navigationbar")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult NavigationBar()
        {
            return this.PartialView("_NavigationSidebar");
        }
    }
}