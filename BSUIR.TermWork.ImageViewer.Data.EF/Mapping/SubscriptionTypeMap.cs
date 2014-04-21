// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionTypeMap.cs" company="">
//   
// </copyright>
// <summary>
//   The subscription type map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The subscription type map.
    /// </summary>
    internal sealed class SubscriptionTypeMap : EntityTypeConfiguration<SubscriptionType>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionTypeMap"/> class.
        /// </summary>
        public SubscriptionTypeMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.Description)
                .HasMaxLength(SubscriptionType.MaxLengthFor.Description);
            this.Property(p => p.Name).IsRequired();
        }

        #endregion
    }
}