namespace BSUIR.TermWork.ImageViewer.Model
{
    public class ImageContent : Entity<int>
    {
        #region Fields

        #endregion

        #region Public Properties

        public byte[] Content { get; set; }

        public byte[] Thumbnail { get; set; }

        #endregion

        public static class MaxLengthFor
        {
            #region Constants

            public const int FileSize = 2097152;
            public const int Width = 300;
            public const int Height = 300;

            #endregion
        }
    }
}