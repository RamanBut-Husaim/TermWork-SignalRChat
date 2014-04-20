using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    internal sealed class ImageContentMap : EntityTypeConfiguration<ImageContent>
    {
        public ImageContentMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Content).IsRequired().HasMaxLength(ImageContent.MaxLengthFor.FileSize);
            this.Property(p => p.Thumbnail).IsRequired().HasMaxLength(ImageContent.MaxLengthFor.FileSize);
        }
    }
}
