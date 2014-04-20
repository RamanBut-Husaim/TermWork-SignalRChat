using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    internal sealed class TagMap : EntityTypeConfiguration<Tag>
    {
        #region Constructors and Destructors

        public TagMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Content).IsRequired().HasMaxLength(Tag.MaxLengthFor.Content);
        }

        #endregion
    }
}