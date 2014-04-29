using System.Globalization;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;

namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters
{
    public sealed class ViewBagPopulatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userKeyString =
                filterContext.Controller.ControllerContext.RouteData.Values["key"] as string;
            filterContext.Controller.ViewBag.Key = userKeyString;
            filterContext.Controller.ViewBag.AlbumKey =
                filterContext.Controller.ControllerContext.RouteData.Values["albumKey"];
            filterContext.Controller.ViewBag.ImageKey =
                filterContext.Controller.ControllerContext.RouteData.Values["imageKey"];
            var user =
                filterContext.Controller.ControllerContext.HttpContext.User.Identity as
                CustomIdentity;
            if (user != null && userKeyString != null)
            {
                if (userKeyString.Equals(user.Id.ToString(CultureInfo.InvariantCulture)))
                {
                    filterContext.Controller.ViewBag.IsVisitor = false;
                }
                else
                {
                    filterContext.Controller.ViewBag.IsVisitor = true;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
