// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageMap.cs" company="">
//   
// </copyright>
// <summary>
//   The image map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The image map.
    /// </summary>
    internal sealed class ImageMap : EntityTypeConfiguration<Image>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMap"/> class.
        /// </summary>
        public ImageMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Name).IsRequired().HasMaxLength(Image.MaxLengthFor.Name);
            this.Property(p => p.Extension).IsRequired().HasMaxLength(Image.MaxLengthFor.Extension);
            this.Property(p => p.Description)
                .IsOptional()
                .HasMaxLength(Image.MaxLengthFor.Description);
            this.Property(p => p.Extension).IsRequired().HasMaxLength(Image.MaxLengthFor.Extension);
            this.Property(p => p.Rate).IsRequired();
            this.Property(p => p.ContentType)
                .IsRequired()
                .HasMaxLength(Image.MaxLengthFor.ContentType);
            this.Property(p => p.UploadDate).IsRequired();
            this.HasRequired(p => p.Owner).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(p => p.Album).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(p => p.ImageContent).WithRequiredPrincipal().WillCascadeOnDelete(false);
            this.HasMany(p => p.Tags).WithMany().Map(
                m =>
                    {
                        m.MapLeftKey("ImageKey");
                        m.MapRightKey("TagKey");
                        m.ToTable("TagImage");
                    });
        }

        #endregion
    }
}