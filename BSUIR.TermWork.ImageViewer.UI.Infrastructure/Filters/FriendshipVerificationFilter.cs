using System.Linq;
using System.Web.Mvc;

namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters
{
    public sealed class FriendshipVerificationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userKeyString =
                filterContext.Controller.ControllerContext.RouteData.Values["key"] as string;
            int key;
            if (int.TryParse(userKeyString, out key))
            {
                var friendTargets =
                    filterContext.HttpContext.Session[Constants.SessionFriendTargets] as int[];
                if (friendTargets != null)
                {
                    filterContext.Controller.ViewBag.IsFriend =
                        friendTargets.Where(p => p.Equals(key)).Any();
                }
            }
        }
    }
}
