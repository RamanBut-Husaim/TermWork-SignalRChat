using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.UI.WebSite.Mappers
{
    using System.Web;

    using BSUIR.TermWork.ImageViewer.Model;
    using BSUIR.TermWork.ImageViewer.UI.WebSite.ViewModel.Image;

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
