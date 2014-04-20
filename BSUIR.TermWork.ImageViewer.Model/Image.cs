using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public class Image : Entity<int>
    {
        #region Fields

        private string _extension;
        private string _name;
        private DateTime _uploadDate;

        #endregion

        #region Constructors and Destructors

        public Image()
            : this(string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        public Image(string name, string extension)
            : this(name, extension, string.Empty, string.Empty)
        {
        }

        public Image(string name, string extension, string contentType)
            : this(name, extension, contentType, string.Empty)
        {
        }

        public Image(string name, string extension, string contentType, string description)
        {
            this._name = name;
            this._extension = extension;
            this.ContentType = contentType;
            this.Description = description;
            this.Rate = 0;
        }

        #endregion

        #region Public Properties

        public virtual Album Album { get; set; }

        public string ContentType { get; set; }

        public string Description { get; set; }

        public string Extension
        {
            get { return this._extension; }
            set { this._extension = value; }
        }

        public virtual ImageContent ImageContent { get; set; }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public virtual User Owner { get; set; }

        public int Rate { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public DateTime UploadDate
        {
            get { return this._uploadDate; }
            set { this._uploadDate = value; }
        }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return string.Format("{0}{1}", this._name, this._extension);
        }

        #endregion

        public static class MaxLengthFor
        {
            #region Constants

            public const int ContentType = 30;
            public const int Description = 500;
            public const int Extension = 30;
            public const int Name = 250;
            public const int RateMin = 0;
            public const int RateMax = 5;

            #endregion

            #region Static Fields

            public static readonly DateTime MinSqlDateTime = SqlDateTime.MinValue.Value;

            #endregion
        }
    }
}