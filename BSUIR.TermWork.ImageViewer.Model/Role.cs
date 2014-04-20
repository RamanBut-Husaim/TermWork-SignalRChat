using System.Collections.Generic;

namespace BSUIR.TermWork.ImageViewer.Model
{
    public class Role : Entity<int>
    {
        #region Fields

        private string _description;
        private RoleName _name;

        #endregion

        #region Constructors and Destructors

        public Role()
        {
        }

        public Role(RoleName name, string description)
        {
            this._name = name;
            this._description = description;
        }

        #endregion

        #region Public Properties

        public virtual ICollection<AccessRight> AccessRights { get; set; }

        public string Description
        {
            get { return this._description; }
            set { this._description = value ?? this._name.ToString(); }
        }

        public RoleName Name
        {
            get { return this._name; }
            private set { this._name = value; }
        }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return this._name.ToString();
        }

        #endregion

        public static class MaxLengthFor
        {
            #region Constants

            public const int Description = 100;
            public const int Name = 50;

            #endregion
        }
    }
}