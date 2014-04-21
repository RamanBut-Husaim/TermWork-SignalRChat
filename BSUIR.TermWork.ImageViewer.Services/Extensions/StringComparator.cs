// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringComparator.cs" company="">
//   
// </copyright>
// <summary>
//   The string comparator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;

namespace BSUIR.TermWork.ImageViewer.Services.Extensions
{
    /// <summary>
    /// The string comparator.
    /// </summary>
    public sealed class StringComparator : IEqualityComparer<string>
    {
        #region Static Fields

        /// <summary>
        /// The comparator.
        /// </summary>
        private static readonly StringComparator Comparator = new StringComparator();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="StringComparator"/> class from being created.
        /// </summary>
        private StringComparator()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static StringComparator Instance
        {
            get { return Comparator; }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(string x, string y)
        {
            return x.Equals(y, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}