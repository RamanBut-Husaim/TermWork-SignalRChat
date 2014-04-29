using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Search;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public interface ISearchMapper
    {
        SearchResultViewModel BuildResult(Album album);

        SearchResultViewModel BuildResult(Image image);
    }
}
