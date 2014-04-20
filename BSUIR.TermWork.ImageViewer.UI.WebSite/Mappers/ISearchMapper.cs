using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    using BSUIR.TermWork.ImageViewer.Model;
    using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Search;

    public interface ISearchMapper
    {
        SearchResultViewModel BuildResult(Album album);

        SearchResultViewModel BuildResult(Image image);
    }
}
