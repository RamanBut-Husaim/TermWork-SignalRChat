using System.Web.Mvc;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
