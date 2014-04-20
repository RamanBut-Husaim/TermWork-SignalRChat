using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.UI.Infrastructure
{
    using System.IO;

    public interface IImageConverter
    {
        string ConvertToBase64(Stream imageStream);
    }
}
