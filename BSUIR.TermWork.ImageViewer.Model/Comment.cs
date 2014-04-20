using System;
using System.Data.SqlTypes;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public class Comment : Entity<int>
    {
        #region Fields

        private DateTime _creationDate;

        #endregion

        #region Constructors and Destructors

        public Comment()
            : this(string.Empty, null, DateTime.Now)
        {
        }

        public Comment(string content)
            : this(content, null, DateTime.Now)
        {
        }

        public Comment(string content, Image image)
            : this(content, image, DateTime.Now)
        {
        }

        public Comment(string content, Image image, DateTime creationDate)
        {
            this.Content = content;
            this.Image = image;
            this._creationDate = creationDate;
            this.Rate = 0;
        }

        #endregion

        #region Public Properties

        public string Content { get; set; }

        public DateTime CreationDate
        {
            get { return this._creationDate; }
            set { this._creationDate = value; }
        }

        public Image Image { get; set; }

        public virtual User Owner { get; set; }

        public int Rate { get; set; }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return string.Format(
                "User: {0} CreationDate: {1}",
                (this.Owner != null) ? this.Owner.Email : "undefined",
                this._creationDate.ToShortDateString());
        }

        #endregion

        public static class MaxLengthFor
        {
            #region Constants

            public const int Content = 500;
            public const int RateMin = 0;
            public const int RateMax = 5;

            #endregion

            #region Static Fields

            public static readonly DateTime MinSqlDateTime = SqlDateTime.MinValue.Value;

            #endregion
        }
    }
}