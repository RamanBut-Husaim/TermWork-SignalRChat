using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Helpers;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    public abstract class BaseController : Controller
    {
        protected virtual void UpdateSubscriptions(ISubscriptionService subscriptionService)
        {
            var user = User.Identity as CustomIdentity;
            if (user != null)
            {
                try
                {
                    IList<Subscription> subscriptions =
                        subscriptionService.GetFilteredSubscriptionsForUser(user.Id);
                    this.Session[Constants.SessionSubscriptionTargets] =
                        subscriptions.Select(p => p.Target.Key).ToArray();
                }
                catch (Exception ex)
                {
                    this.Session[Constants.SessionSubscriptionTargets] = new int[0];
                } 
            }
        }

        protected virtual void UpdateFriendsList(IFriendshipService friendshipService)
        {
            var user = this.User.Identity as CustomIdentity;
            try
            {
                this.Session[Constants.SessionFriendTargets] =
                    friendshipService.GetFriends(user.Id).Select(p => p.User.Key).ToArray();
            }
            catch (Exception ex)
            {
                this.Session[Constants.SessionFriendTargets] = new int[0];
            }
        }

        protected virtual void ReleaseUpToDateSubscriptions(
            IMembershipService membershipService,
            ISubscriptionService subscriptionService)
        {
            var user = User.Identity as CustomIdentity;
            var keyString = RouteData.Values["key"] as string;
            int key;
            if (int.TryParse(keyString, out key) && user != null && !key.Equals(user.Id))
            {
                try
                {
                    User subscriber = membershipService.GetUserByKey(user.Id);
                    User target = membershipService.GetUserByKey(key);
                    subscriptionService.ResetNewSubscriptions(subscriber, target);
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            String cultureName = null;

            HttpCookie cultureCookie = requestContext.HttpContext.Request.Cookies["_culture"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureName = requestContext.HttpContext.Request.UserLanguages[0];
            }

            cultureName = CultureHelper.GetImplementedCulture(cultureName);

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            base.Initialize(requestContext);
        }
    }
}