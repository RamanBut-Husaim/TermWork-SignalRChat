// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageContentMap.cs" company="">
//   
// </copyright>
// <summary>
//   The image content map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The image content map.
    /// </summary>
    internal sealed class ImageContentMap : EntityTypeConfiguration<ImageContent>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageContentMap"/> class.
        /// </summary>
        public ImageContentMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(ImageContent.MaxLengthFor.FileSize);
            this.Property(p => p.Thumbnail)
                .IsRequired()
                .HasMaxLength(ImageContent.MaxLengthFor.FileSize);
        }

        #endregion
    }
}