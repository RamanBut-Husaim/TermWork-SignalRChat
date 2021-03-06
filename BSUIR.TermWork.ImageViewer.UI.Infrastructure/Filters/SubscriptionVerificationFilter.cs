﻿using System.Linq;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;

namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters
{
    public sealed class SubscriptionVerificationFilter : ActionFilterAttribute
    {
        #region Overrides of ActionFilterAttribute

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userKeyString =
                filterContext.Controller.ControllerContext.RouteData.Values["key"] as string;
            var user = filterContext.HttpContext.User.Identity as CustomIdentity;
            int key;
            if (int.TryParse(userKeyString, out key))
            {
                var subscribtionTargets =
                    filterContext.HttpContext.Session[Constants.SessionSubscriptionTargets] as int[];
                if (subscribtionTargets != null)
                {
                    filterContext.Controller.ViewBag.IsSubscribed =
                        subscribtionTargets.Where(p => p.Equals(key)).Any();
                }
            }

            base.OnActionExecuting(filterContext);
        }

        #endregion
    }
}
