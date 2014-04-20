using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    internal sealed class AccessRightMap : EntityTypeConfiguration<AccessRight>
    {
        public AccessRightMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(AccessRight.MaxLengthFor.Description);
        }
    }
}
