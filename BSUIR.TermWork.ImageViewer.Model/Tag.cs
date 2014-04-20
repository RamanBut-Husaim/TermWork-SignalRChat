namespace BSUIR.TermWork.ImageViewer.Model
{
    public class Tag : Entity<int>
    {
        #region Fields

        private string _content;

        #endregion

        #region Constructors and Destructors

        public Tag()
            : this(string.Empty)
        {
        }

        public Tag(string content)
        {
            this._content = content;
        }

        #endregion

        #region Public Properties

        public string Content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return string.Format("{0}", this._content);
        }

        #endregion

        public static class MaxLengthFor
        {
            #region Constants

            public const int Content = 100;
            public const string WordSplitting = @"[^\p{L}]*\p{Z}[^\p{L}]*";
            public const string PunctuationSplitter = @"[ .,-?!(){}<>]*[\W]";
            public const int MinLength = 3;

            #endregion
        }
    }
}