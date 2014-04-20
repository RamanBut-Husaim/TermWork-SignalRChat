using System.Data.Entity;

using BSUIR.TermWork.ImageViewer.Data.EF.Initialization;
using BSUIR.TermWork.ImageViewer.Data.EF.Mapping;
using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF
{
    public sealed class ImageViewerContext : DbContextBase
    {
        #region Constructors and Destructors

        static ImageViewerContext()
        {
            Database.SetInitializer(
                //new DropCreateDatabaseIfModelChanges<ImageViewerContext>()
                //new InitializeImageViewerAlways()
                new InitializeImageViewerIfModelChanges());
        }

        public ImageViewerContext()
            : base("ImageViewer")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new AccessRightMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new AlbumMap());
            modelBuilder.Configurations.Add(new ImageContentMap());
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new SubscriptionMap());
            modelBuilder.Configurations.Add(new SubscriptionTypeMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new FriendshipMap());
            modelBuilder.Configurations.Add(new FriendshipRequestMap());
            modelBuilder.Configurations.Add(new MessageMap());
        }

        #endregion
    }
}