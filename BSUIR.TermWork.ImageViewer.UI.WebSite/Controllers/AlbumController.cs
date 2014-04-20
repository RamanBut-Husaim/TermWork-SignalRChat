using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Filters;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Album;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [RoutePrefix("user/{key:int}")]
    [CustomAuthFilter]
    public class AlbumController : BaseController
    {
        private readonly IMembershipService _membershipService;
        private readonly IImageAlbumService _imageAlbumService;
        private readonly IAlbumMapper _albumMapper;

        public AlbumController(
            IMembershipService membershipService,
            IImageAlbumService imageAlbumService,
            IAlbumMapper albumMapper)
        {
            this._membershipService = membershipService;
            this._imageAlbumService = imageAlbumService;
            this._albumMapper = albumMapper;
        }

        [HttpGet]
        [ChildActionOnly]
        [ViewBagPopulatorFilter]
        [Route("album/list")]
        public PartialViewResult ListAlbums(int? key)
        {
            IList<AlbumListItemViewModel> result = new List<AlbumListItemViewModel>();

            try
            {
                IList<Album> albums = this._imageAlbumService.GetAlbumsByUserKey(key.Value);
                result = albums.Select(p => this._albumMapper.BuildListItem(p)).ToList();
            }
            catch (Exception ex)
            {
                this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_ListAlbums", result);
        }

        [HttpGet]
        [ChildActionOnly]
        [ViewBagPopulatorFilter]
        [Route("album/headers")]
        public PartialViewResult AlbumHeaders(int? key)
        {
           // ViewBag.Key = key;
            IList<AlbumHeaderViewModel> result = new List<AlbumHeaderViewModel>();
            try
            {
                IList<Album> albums = this._imageAlbumService.GetAlbumsByUserKey(key.Value);
                result = albums.Select(p => this._albumMapper.BuildAlbumHeader(p)).ToList();
            }
            catch (Exception ex)
            {
            }

            return this.PartialView("_AlbumHeaders", result);
        }

        [HttpGet]
        [Route("createalbum")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public PartialViewResult CreateAlbum()
        {
            var result = new AlbumCreateViewModel();
            return this.PartialView("_CreateAlbum", result);
        }

        [HttpGet]
        [Route("editalbum/{albumKey}")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public PartialViewResult EditAlbum(int? albumKey)
        {
            AlbumCreateViewModel result = null;

            if (albumKey.HasValue)
            {
                try
                {
                    Album album = this._imageAlbumService.GetAlbumByKey(albumKey.Value);
                    if (album != null)
                    {
                        result = this._albumMapper.BuildCreateAlbum(album);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "Album is missing.";
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
                this.TempData[Constants.TempDataErrorMessage] = "Album is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_EditAlbum", result);
        }

        [HttpPost]
        [Route("editalbum/{albumkey}")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public ActionResult EditAlbum(AlbumCreateViewModel viewModel)
        {
            bool result = false;
            int key;

            if (!int.TryParse(ViewBag.Key, out key))
            {
                key = -1;
            }

            var user = this.User.Identity as CustomIdentity;

            if (this.ModelState.IsValid && viewModel != null)
            {
                try
                {
                    User owner = this._membershipService.GetUserByKey(key);
                    Album sourceAlbum = this._imageAlbumService.GetAlbumByKey(viewModel.Key);
                    if (sourceAlbum != null)
                    {
                        this._albumMapper.UpdateAlbum(sourceAlbum, viewModel);
                        this._imageAlbumService.UpdateAlbum(owner, sourceAlbum);
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

            return this.PartialView("_EditAlbum", viewModel);
        }

        [HttpGet]
        [Route("detailedview/{albumKey:int}")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult DetailedView(int? albumKey)
        {
            var result = new AlbumDetailedViewModel();
            if (albumKey.HasValue)
            {
                try
                {
                    Album album = this._imageAlbumService.GetAlbumByKey(albumKey.Value);
                    if (album != null)
                    {
                        result = this._albumMapper.BuildDetailed(album);
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
        [Route("removealbum/{albumKey}")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public PartialViewResult RemoveAlbum(int? albumKey)
        {
            AlbumCreateViewModel result = null;

            if (albumKey.HasValue)
            {
                try
                {
                    Album album = this._imageAlbumService.GetAlbumByKey(albumKey.Value);
                    if (album != null)
                    {
                        result = this._albumMapper.BuildCreateAlbum(album);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "Album is missing.";
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
                this.TempData[Constants.TempDataErrorMessage] = "Album is missing.";
                return this.PartialView("_ErrorModal");
            }

            return this.PartialView("_RemoveAlbum", result);
        }

        [HttpPost]
        [Route("removealbum/{albumKey}")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public ActionResult RemoveAlbum(AlbumCreateViewModel viewModel)
        {
            bool result = false;
            if (viewModel != null)
            {
                try
                {
                    Album album = this._imageAlbumService.GetAlbumByKey(viewModel.Key);
                    if (album != null)
                    {
                        result = this._imageAlbumService.RemoveAlbum(album);
                    }
                    else
                    {
                        this.TempData[Constants.TempDataErrorMessage] = "Album is missing.";
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
                this.TempData[Constants.TempDataErrorMessage] = "Album is missing.";
                return this.PartialView("_ErrorModal");
            }

            if (result)
            {
                return this.Json(new { success = result });
            }

            return this.PartialView("_Success");
        }

        [HttpPost]
        [Route("createalbum")]
        [ViewBagPopulatorFilter]
        [AjaxRequestOnlyFilter]
        public ActionResult CreateAlbum(AlbumCreateViewModel viewModel)
        {
            bool result = false;
            var user = this.User.Identity as CustomIdentity;
            if (this.ModelState.IsValid && viewModel != null)
            {
                try
                {
                    User owner = this._membershipService.GetUserByKey(user.Id);
                    Album album = this._albumMapper.BuildAlbum(viewModel);
                    this._imageAlbumService.CreateAlbum(owner, album);
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

            return this.PartialView("_CreateAlbum", viewModel);
        }

        [HttpGet]
        [Route("albumactions")]
        [ViewBagPopulatorFilter]
        [ChildActionOnly]
        public PartialViewResult AlbumActions()
        {
            return this.PartialView("_AlbumNavbarActionList");
        }
    }
}