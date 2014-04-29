using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [CustomAuthFilter]
    [RoutePrefix("user/{key:int}/operations")]
    public sealed class OperationController : BaseController
    {
        private readonly IFriendshipService _friendshipService;

        public OperationController(IFriendshipService friendshipService)
        {
            this._friendshipService = friendshipService;
        }

        [HttpGet]
        [Route("friendshipoperation")]
        [ViewBagPopulatorFilter]
        [FriendshipVerificationFilter]
        [ChildActionOnly]
        public PartialViewResult FriendshipOperation()
        {
            return this.PartialView("_FriendshipOperation");
        }

        [HttpGet]
        [Route("operationlist")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult OperationList()
        {
            return this.PartialView("_Operations");
        }
    }
}