// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagMap.cs" company="">
//   
// </copyright>
// <summary>
//   The tag map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The tag map.
    /// </summary>
    internal sealed class TagMap : EntityTypeConfiguration<Tag>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TagMap"/> class.
        /// </summary>
        public TagMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Content).IsRequired().HasMaxLength(Tag.MaxLengthFor.Content);
        }

        #endregion
    }
}