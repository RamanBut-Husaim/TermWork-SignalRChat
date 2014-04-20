using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    internal sealed class AlbumMap : EntityTypeConfiguration<Album>
    {
        #region Constructors and Destructors

        public AlbumMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Name).IsRequired().HasMaxLength(Album.MaxLengthFor.Name);
            this.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Album.MaxLengthFor.Description);
            this.Property(p => p.CreationDate).IsRequired();
            this.Property(p => p.ImageNumber).IsRequired();
            this.HasMany(r => r.Tags).WithMany().Map(
                m =>
                    {
                        m.MapLeftKey("AlbumKey");
                        m.MapRightKey("TagKey");
                        m.ToTable("TagAlbum");
                    });
            this.HasRequired(p => p.Owner).WithMany().WillCascadeOnDelete(true);
        }

        #endregion
    }
}