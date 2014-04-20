using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.TermWork.ImageViewer.UI.Console
{
    using BSUIR.TermWork.ImageViewer.Data.EF;
    using BSUIR.TermWork.ImageViewer.Model;

    class Program
    {
        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.SetData("DataDirectory", @"E:\BSUIR\_Fourth_Year\7-th_term\TermWork\BSUIR.TermWork.ImageViewer");
            using (ImageViewerContext context = new ImageViewerContext())
            {
                var roles = context.Set<Role>().ToList();
                var accessRights = context.Set<AccessRight>().ToList();
            }

        }
    }
}
