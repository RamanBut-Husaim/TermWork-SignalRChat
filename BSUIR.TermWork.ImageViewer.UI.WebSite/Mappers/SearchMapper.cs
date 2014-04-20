namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    using System;

    using BSUIR.TermWork.ImageViewer.Model;
    using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Search;

    public class SearchMapper : ISearchMapper
    {
        #region Implementation of ISearchMapper

        public SearchResultViewModel BuildResult(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException("album");
            }
            var result = new SearchResultViewModel();
            result.CreationDate = album.CreationDate;
            result.Key = album.Key;
            result.Name = album.Name;
            result.Type = "Album";
            result.OwnerName = string.Format(
                "{0} {1}",
                album.Owner.UserProfile.FirstName,
                album.Owner.UserProfile.LastName);
            return result;
        }

        public SearchResultViewModel BuildResult(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("album");
            }
            var result = new SearchResultViewModel();
            result.CreationDate = image.UploadDate;
            result.Key = image.Key;
            result.Name = image.Name;
            result.Type = "Image";
            result.OwnerName = string.Format(
                "{0} {1}",
                image.Owner.UserProfile.FirstName,
                image.Owner.UserProfile.LastName);
            return result;
        }

        #endregion
    }
}