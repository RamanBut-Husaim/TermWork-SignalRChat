namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters
{
    using System.Web.Mvc;

    public sealed class AjaxRequestOnlyFilter : ActionFilterAttribute
    {
        #region Public Methods and Operators

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new RedirectResult("~/home/error");
            }
            base.OnActionExecuting(filterContext);
        }

        #endregion
    }
}