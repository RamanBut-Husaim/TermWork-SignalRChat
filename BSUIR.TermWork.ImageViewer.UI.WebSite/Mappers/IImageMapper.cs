using System.Web;

using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Image;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    public interface IImageMapper
    {
        Image BuildImage(HttpPostedFileBase uploadedImage);

        ImageSliderViewModel BuildSlider(Image image);

        ImageEditViewModel BuildEdit(Image image);

        void UpdateImage(Image image, ImageEditViewModel viewModel);

        ImageDetailedViewModel BuildDetailed(Image image);

        ImagePreviewViewModel BuildPreview(Image image);
    }
}
