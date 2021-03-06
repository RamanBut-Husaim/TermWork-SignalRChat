﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubscriptionMap.cs" company="">
//   
// </copyright>
// <summary>
//   The subscription map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Data.Entity.ModelConfiguration;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Data.EF.Mapping
{
    /// <summary>
    /// The subscription map.
    /// </summary>
    internal sealed class SubscriptionMap : EntityTypeConfiguration<Subscription>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionMap"/> class.
        /// </summary>
        public SubscriptionMap()
        {
            this.HasKey(p => p.Key);
            this.Property(p => p.Key).IsRequired();
            this.Property(p => p.CreationDate).IsRequired();
            this.HasRequired(p => p.Subscriber).WithOptional().WillCascadeOnDelete(false);
            this.HasRequired(p => p.Type).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(p => p.Subscriber).WithMany().WillCascadeOnDelete(false);
            this.HasRequired(p => p.Target).WithMany().WillCascadeOnDelete(false);
        }

        #endregion
    }
}