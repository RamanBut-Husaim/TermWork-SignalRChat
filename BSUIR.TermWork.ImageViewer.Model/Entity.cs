namespace BSUIR.TermWork.ImageViewer.Model
{
    public abstract class Entity<TKey> : EntityBase
    {
        #region Fields

        #endregion

        #region Constructors and Destructors

        protected Entity()
        {
        }

        protected Entity(TKey key)
        {
            this.Key = key;
        }

        #endregion

        #region Public Properties

        public TKey Key { get; protected set; }

        #endregion
    }
}