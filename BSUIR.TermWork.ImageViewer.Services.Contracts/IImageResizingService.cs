namespace BSUIR.TermWork.ImageViewer.Services.Contracts
{
    public interface IImageResizingService : IService
    {
        #region Public Methods and Operators

        byte[] ResizeImage(byte[] source, int width, int height);

        #endregion
    }
}