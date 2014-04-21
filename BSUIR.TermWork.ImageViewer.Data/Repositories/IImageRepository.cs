// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImageRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The ImageRepository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.Repositories
{
    /// <summary>
    /// The ImageRepository interface.
    /// </summary>
    public interface IImageRepository : IRepository<Image, int>
    {
    }
}