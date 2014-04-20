namespace BSUIR.TermWork.ImageViewer.Model
{
    public class AccessRight : Entity<int>
    {
        #region Fields

        private AccessRightName _name;

        #endregion

        public AccessRight()
        {
        }

        public AccessRight(AccessRightName name, string description)
        {
            this._name = name;
            this.Description = description;
        }

        #region Public Properties

        public string Description { get; set; }

        public AccessRightName Name
        {
            get { return this._name; }
            private set { this._name = value; }
        }

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

            public const int Description = 100;
            public const int Name = 50;

            #endregion
        }
    }
}