using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Comment;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [RoutePrefix("user/{key:int}/album/{albumKey:int}/image/{imageKey:int}")]
    [CustomAuthFilter]
    public class CommentController : BaseController
    {
        private readonly ICommentMapper _commentMapper;
        private readonly IImageAlbumService _imageAlbumService;
        private readonly ICommentService _commentService;
        private readonly IMembershipService _membershipService;

        public CommentController(ICommentMapper commentMapper, IImageAlbumService imageAlbumService, ICommentService commentService, IMembershipService membershipService)
        {
            this._commentMapper = commentMapper;
            this._imageAlbumService = imageAlbumService;
            this._commentService = commentService;
            this._membershipService = membershipService;
        }

        [HttpGet]
        [Route("comments")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult Comments(int? imageKey)
        {
            //ViewBag.Key = RouteData.Values["key"];
            //ViewBag.AlbumKey = RouteData.Values["albumKey"];
            //ViewBag.ImageKey = RouteData.Values["imageKey"];

            IList<CommentListItemViewModel> result = new List<CommentListItemViewModel>();

            if (imageKey.HasValue)
            {
                try
                {
                    Image image = this._imageAlbumService.GetImageByKey(imageKey.Value);
                    if (image != null)
                    {
                        result =
                            this._commentService.GetCommentsForImage(image)
                                .Select(p => this._commentMapper.BuildListItem(p))
                                .ToList();
                    }
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }
            return this.PartialView("_ListComment", result);
        }

        [HttpGet]
        [Route("comment/add")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public PartialViewResult CreateComment(int? imageKey)
        {
            //ViewBag.Key = RouteData.Values["key"];
            //ViewBag.AlbumKey = RouteData.Values["albumKey"];
            //ViewBag.ImageKey = RouteData.Values["imageKey"];

            var result = new CommentCreateViewModel();
            return this.PartialView("_CreateComment", result);
        }

        [HttpPost]
        [Route("comment/add")]
        [AjaxRequestOnlyFilter]
        public ActionResult CreateComment(int? imageKey, CommentCreateViewModel viewModel)
        {
            bool result = false;
            var user = this.User.Identity as CustomIdentity;
            if (this.ModelState.IsValid && viewModel != null)
            {
                try
                {
                    User owner = this._membershipService.GetUserByKey(user.Id);
                    Image image = this._imageAlbumService.GetImageByKey(imageKey.Value);
                    Comment comment = this._commentMapper.BuildComment(viewModel);
                    comment.Owner = owner;
                    comment.Image = image;
                    this._commentService.CreateComment(owner, image, comment);
                    result = true;
                }
                catch (Exception)
                {
                    this.ModelState.AddModelError(string.Empty, Resources.Resources.OperationFailure);
                }
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, Resources.Resources.OperationFailure);
            }

            if (result)
            {
                return this.Json(new { success = result });
            }
            return this.PartialView("_CreateComment", viewModel);
        }

        [HttpGet]
        [Route("comment/remove/{commentKey:int}")]
        [AjaxRequestOnlyFilter]
        public PartialViewResult RemoveComment(int? commentKey)
        {
            CommentCreateViewModel result = null;
            if (commentKey.HasValue)
            {
                try
                {
                    Comment comment = this._commentService.GetCommentByKey(commentKey.Value);
                    if (comment != null)
                    {
                        result = this._commentMapper.BuildCreate(comment);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "Comment is missing.";
                        return this.PartialView("_ErrorModal");
                    }
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }
            else
            {
                this.TempData[Constants.TempDataErrorMessage] = "Comment is missing.";
                return this.PartialView("_ErrorModal");
            }
            return this.PartialView("_RemoveComment", result);
        }

        [HttpPost]
        [Route("comment/remove/{commentKey:int}")]
        [AjaxRequestOnlyFilter]
        public ActionResult RemoveComment(CommentCreateViewModel viewModel)
        {
            bool result = false;
            if (viewModel != null)
            {
                try
                {
                    Comment comment = this._commentService.GetCommentByKey(viewModel.Key);
                    if (comment != null)
                    {
                        result = this._commentService.RemoveComment(comment);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "Comment is missing.";
                        return this.PartialView("_ErrorModal");
                    }
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }
            else
            {
                this.TempData[Constants.TempDataErrorMessage] = "Comment is missing.";
                return this.PartialView("_ErrorModal");
            }
            if (result)
            {
                return this.Json(new { success = result });
            }
            return this.PartialView("_Success");
        }
	}
}