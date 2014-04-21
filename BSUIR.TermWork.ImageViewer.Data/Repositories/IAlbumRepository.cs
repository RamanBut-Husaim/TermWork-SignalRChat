// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAlbumRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The AlbumRepository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.Repositories
{
    /// <summary>
    /// The AlbumRepository interface.
    /// </summary>
    public interface IAlbumRepository : IRepository<Album, int>
    {
    }
}