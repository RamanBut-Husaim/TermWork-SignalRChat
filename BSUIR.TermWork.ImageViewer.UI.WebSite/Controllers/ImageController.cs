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
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Image;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [RoutePrefix("user/{key:int}/album/{albumKey:int}")]
    [CustomAuthFilter]
    public class ImageController : BaseController
    {
        #region Fields

        private readonly IImageAlbumService _imageAlbumService;
        private readonly IImageMapper _imageMapper;
        private readonly IMembershipService _membershipService;
        private readonly ISubscriptionService _subscriptionService;

        #endregion

        #region Constructors and Destructors

        public ImageController(
            IImageAlbumService imageAlbumService,
            IMembershipService membershipService,
            ISubscriptionService subscriptionService,
            IImageMapper imageMapper)
        {
            this._membershipService = membershipService;
            this._imageAlbumService = imageAlbumService;
            this._subscriptionService = subscriptionService;
            this._imageMapper = imageMapper;
        }

        #endregion

        #region Public Methods and Operators

        [HttpGet]
        [Route("detailedview/{imageKey:int}")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult DetailedView(int? imageKey)
        {
            var result = new ImageDetailedViewModel();
            if (imageKey.HasValue)
            {
                try
                {
                    Image image = this._imageAlbumService.GetImageByKey(imageKey.Value);
                    if (image != null)
                    {
                        result = this._imageMapper.BuildDetailed(image);
                    }
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }

            return this.PartialView("_NavigationBarDetailedView", result);
        }

        [HttpGet]
        [AjaxRequestOnlyFilter]
        [ViewBagPopulatorFilter]
        [Route("editimage/{imageKey:int}")]
        public PartialViewResult EditImage(int? key, int? albumKey, int? imageKey)
        {
            ImageEditViewModel result;

            if (imageKey.HasValue)
            {
                try
                {
                    Image image = this._imageAlbumService.GetImageByKey(imageKey.Value);
                    if (image != null)
                    {
                        result = this._imageMapper.BuildEdit(image);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "Image is missing.";
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
                this.TempData[Constants.TempDataErrorMessage] = "Image is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_EditImage", result);
        }

        [HttpPost]
        [Route("editimage/{imageKey:int}")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public ActionResult EditImage(int? albumKey, int? imageKey, ImageEditViewModel viewModel)
        {
            bool result = false;
            int key;
            if (!int.TryParse(ViewBag.Key, out key))
            {
                key = -1;
            }

            if (this.ModelState.IsValid && viewModel != null)
            {
                try
                {
                    User owner = this._membershipService.GetUserByKey(key);
                    Album album = this._imageAlbumService.GetAlbumByKey(albumKey.Value);
                    Image sourceImage = this._imageAlbumService.GetImageByKey(imageKey.Value);
                    if (sourceImage != null)
                    {
                        this._imageMapper.UpdateImage(sourceImage, viewModel);
                        this._imageAlbumService.UpdateImage(owner, album, sourceImage);
                    }
                    result = true;
                }
                catch (Exception ex)
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

            return this.PartialView("_EditImage", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        [Route("imageactions")]
        [ViewBagPopulatorFilter]
        public PartialViewResult ImageActions()
        {
            return this.PartialView("_ImageActions");
        }

        [HttpGet]
        [ChildActionOnly]
        [Route("imagenavigationbar/{imageKey:int}/actionlist")]
        [ViewBagPopulatorFilter]
        public PartialViewResult ImageNavbarActionList(int? imageKey)
        {
            return this.PartialView("_ImageNavbarActionList");
        }

        [HttpGet]
        [Route("imagenavigationbar/{imageKey:int}")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult ImageNavigationBar(int? imageKey)
        {
            return this.PartialView("_ImageNavigationSidebar");
        }

        [HttpGet]
        [Route("image/{imageKey:int}")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult ImagePreview(int? imageKey)
        {
            var result = new ImagePreviewViewModel();
            if (imageKey.HasValue)
            {
                try
                {
                    Image image = this._imageAlbumService.GetImageByKey(imageKey.Value);
                    if (image != null)
                    {
                        result = this._imageMapper.BuildPreview(image);
                    }
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }

            return this.PartialView("_ImagePreview", result);
        }

        [HttpGet]
        [ChildActionOnly]
        [Route("imageslider")]
        [ViewBagPopulatorFilter]
        public ActionResult ImageSlider(int? key, int? albumKey)
        {
            IList<ImageSliderViewModel> result = new List<ImageSliderViewModel>();
            try
            {
                User owner = this._membershipService.GetUserByKey(key.Value);
                Album album = this._imageAlbumService.GetAlbumByKey(albumKey.Value);
                if (owner != null && album != null)
                {
                    result =
                        this._imageAlbumService.GetAlbumImages(album)
                            .Select(p => this._imageMapper.BuildSlider(p))
                            .ToList();
                }
            }
            catch (Exception ex)
            {
            }

            return this.PartialView("_ImageSlider", result);
        }

        [HttpGet]
        [Route("image/index")]
        [ViewBagPopulatorFilter]
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

        [HttpGet]
        [Route("removeimage/{imageKey:int}")]
        [AjaxRequestOnlyFilter]
        public PartialViewResult RemoveImage(int? imageKey)
        {
            ImageEditViewModel result = null;
            if (imageKey.HasValue)
            {
                try
                {
                    Image image = this._imageAlbumService.GetImageByKey(imageKey.Value);
                    if (image != null)
                    {
                        result = this._imageMapper.BuildEdit(image);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "Image is missing.";
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
                this.TempData[Constants.TempDataErrorMessage] = "Image is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_RemoveImage", result);
        }

        [HttpPost]
        [Route("removeimage/{imageKey:int}")]
        [AjaxRequestOnlyFilter]
        public ActionResult RemoveImage(ImageEditViewModel viewModel)
        {
            bool result = false;
            if (viewModel != null)
            {
                try
                {
                    Image image = this._imageAlbumService.GetImageByKey(viewModel.Key);
                    if (image != null)
                    {
                        result = this._imageAlbumService.RemoveImage(image);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "Image is missing.";
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
                this.TempData[Constants.TempDataErrorMessage] = "Image is missing.";
                return this.PartialView("_ErrorModal");
            }
            if (result)
            {
                return this.Json(new { success = result });
            }

            return this.PartialView("_Success");
        }

        [HttpPost]
        [Route("uploadfile")]
        public ActionResult UploadImage(int? key, int? albumKey, HttpPostedFileBase uploadedImage)
        {
            try
            {
                User owner = this._membershipService.GetUserByKey(key.Value);
                Album album = this._imageAlbumService.GetAlbumByKey(albumKey.Value);
                if (owner != null && album != null)
                {
                    Image result = this._imageMapper.BuildImage(uploadedImage);
                    result.Owner = owner;
                    result.Album = album;
                    this._imageAlbumService.UploadImage(result);
                }
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Index", "Image", new { key, albumKey });
        }

        [HttpGet]
        [ViewBagPopulatorFilter]
        [Route("view/{imageKey:int}")]
        public ActionResult ViewImage(int? key, int? imageKey)
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

            return this.View("ViewImage");
        }

        #endregion
    }
}