using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    internal sealed class RoleMap : EntityTypeConfiguration<Role>
    {
        #region Constructors and Destructors

        public RoleMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Description).HasMaxLength(Role.MaxLengthFor.Description);
            this.HasMany(r => r.AccessRights).WithMany().Map(
                m =>
                    {
                        m.MapLeftKey("AccessRightKey");
                        m.MapRightKey("RoleKey");
                        m.ToTable("AccessRightRole");
                    });
        }

        #endregion
    }
}