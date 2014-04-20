using System.Collections.Generic;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface ISearchService : IService
    {
        IList<Album> SearchAlbums(string searchQuery);

        IList<Image> SearchImages(string searchQuery);
    }
}
