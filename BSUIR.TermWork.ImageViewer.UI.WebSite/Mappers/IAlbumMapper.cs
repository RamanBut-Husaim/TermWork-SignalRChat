using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Album;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{

    public interface IAlbumMapper
    {
        AlbumListItemViewModel BuildListItem(Album album);

        AlbumHeaderViewModel BuildAlbumHeader(Album album);

        Album BuildAlbum(AlbumCreateViewModel viewModel);

        AlbumCreateViewModel BuildCreateAlbum(Album album);

        void UpdateAlbum(Album album, AlbumCreateViewModel viewModel);

        AlbumDetailedViewModel BuildDetailed(Album album);
    }
}
