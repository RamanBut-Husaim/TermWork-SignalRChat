using System;
using System.Collections.Generic;

namespace BSUIR.TermWork.ImageViewer.Services.Extensions
{
    public sealed class StringComparator : IEqualityComparer<string>
    {
        private static readonly StringComparator Comparator = new StringComparator();

        private StringComparator()
        {
        }

        public static StringComparator Instance
        {
            get { return Comparator; }
        }

        #region Implementation of IEqualityComparer<in string>

        public bool Equals(string x, string y)
        {
            return x.Equals(y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}
