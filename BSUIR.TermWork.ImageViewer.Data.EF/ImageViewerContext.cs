// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageViewerContext.cs" company="">
//   
// </copyright>
// <summary>
//   The image viewer context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity;

using BSUIR.TermWork.ImageViewer.Data.EF.Initialization;
using BSUIR.TermWork.ImageViewer.Data.EF.Mapping;

namespace BSUIR.TermWork.ImageViewer.Data.EF
{
    /// <summary>
    /// The image viewer context.
    /// </summary>
    public sealed class ImageViewerContext : DbContextBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="ImageViewerContext"/> class.
        /// </summary>
        static ImageViewerContext()
        {
            Database.SetInitializer(
                // new DropCreateDatabaseIfModelChanges<ImageViewerContext>()
                //new InitializeImageViewerAlways());
            new InitializeImageViewerIfModelChanges());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageViewerContext"/> class.
        /// </summary>
        public ImageViewerContext() : base("ImageViewer")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
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