using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure;
using BSUIR.TermWork.ImageViewer.UI.Infrastructure.Authentication;
using BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Search;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Controllers
{
    [CustomAuthFilter]
    [RoutePrefix("search")]
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly ISearchMapper _searchMapper;

        public SearchController(ISearchService searchService, ISearchMapper searchMapper)
        {
            this._searchService = searchService;
            this._searchMapper = searchMapper;
        }

        [HttpGet]
        [ChildActionOnly]
        [Route("searchbar")]
        public PartialViewResult SearchBar()
        {
            var result = new SearchFormViewModel();
            return this.PartialView("_SearchBar", result);
        }

        [HttpPost]
        [Route("searchbar")]
        public ActionResult PerformSearch(SearchFormViewModel viewModel)
        {
            var result = new List<SearchResultViewModel>();
            if (viewModel != null)
            {
                try
                {
                    IList<Album> albums = this._searchService.SearchAlbums(viewModel.SearchString);
                    IList<Image> images = this._searchService.SearchImages(viewModel.SearchString);
                    for (int i = 0; i < albums.Count; ++i)
                    {
                        SearchResultViewModel model = this._searchMapper.BuildResult(albums[i]);
                        model.Reference = this.Url.Action(
                            "Index",
                            "Image",
                            new { key = albums[i].Owner.Key, albumKey = albums[i].Key });
                        result.Add(model);
                    }

                    for (int i = 0; i < images.Count; ++i)
                    {
                        SearchResultViewModel model = this._searchMapper.BuildResult(images[i]);
                        model.Reference = this.Url.Action(
                            "ViewImage",
                            "Image",
                            new { key = images[i].Owner.Key, albumKey = images[i].Album.Key, imageKey = images[i].Key });
                        result.Add(model);
                    }
                }
                catch (Exception ex)
                {
                    this.TempData[Constants.TempDataErrorMessage] = ex.Message;
                    return this.PartialView("_ErrorModal");
                }
            }

            return this.View("SearchResult", result);
        }
    }
}