using System.Web.Mvc;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : BaseController
    {
        [HttpGet]
        [Route("~/")]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [Route("about")]
        public ActionResult About()
        {
            return this.View();
        }

        [HttpGet]
        [Route("privacy")]
        public ActionResult Privacy()
        {
            return this.View();
        }

        [HttpGet]
        [Route("terms")]
        public ActionResult Terms()
        {
            return this.View();
        }

        [HttpGet]
        [Route("errormodal")]
        public PartialViewResult ErrorModal()
        {
            return this.PartialView("_ErrorModal");
        }

        [HttpGet]
        [Route("error")]
        public ActionResult Error()
        {
            return this.View();
        }
    }
}