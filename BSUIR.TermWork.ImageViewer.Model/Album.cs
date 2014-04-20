using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public class Album : Entity<int>
    {
        #region Fields

        private DateTime _creationDate;
        private string _name;
        private User _owner;

        #endregion

        #region Constructors and Destructors

        public Album()
            : this(string.Empty, null)
        {
        }

        public Album(string name)
            : this(name, null)
        {
        }

        public Album(string name, User owner)
        {
            this._name = name;
            this._owner = owner;
        }

        #endregion

        #region Public Properties

        public DateTime CreationDate
        {
            get { return this._creationDate; }
            set { this._creationDate = value; }
        }

        public string Description { get; set; }

        public int ImageNumber { get; set; }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public virtual User Owner
        {
            get { return this._owner; }
            set { this._owner = value; }
        }

        public virtual ICollection<Tag> Tags { get; set; }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return string.Format("{0}", this._name);
        }

        #endregion

        public static class MaxLengthFor
        {
            #region Constants

            public const int Description = 250;
            public const int Name = 250;

            #endregion

            #region Static Fields

            public static readonly DateTime MinSqlDateTime = SqlDateTime.MinValue.Value;
            #endregion
        }
    }
}