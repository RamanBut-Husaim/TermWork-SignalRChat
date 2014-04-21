// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccessRightMap.cs" company="">
//   
// </copyright>
// <summary>
//   The access right map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The access right map.
    /// </summary>
    internal sealed class AccessRightMap : EntityTypeConfiguration<AccessRight>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessRightMap"/> class.
        /// </summary>
        public AccessRightMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(AccessRight.MaxLengthFor.Description);
        }

        #endregion
    }
}