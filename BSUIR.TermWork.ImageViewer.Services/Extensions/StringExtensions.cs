// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The string extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using BSUIR.TermWork.ImageViewer.Model;

namespace BSUIR.TermWork.ImageViewer.Services.Extensions
{
    /// <summary>
    /// The string extensions.
    /// </summary>
    public static class StringExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The remove duplicates.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public static IList<string> RemoveDuplicates(this IEnumerable<string> input)
        {
            IList<string> result = input.Distinct(StringComparator.Instance).ToList();
            return result;
        }

        /// <summary>
        /// The split words.
        /// </summary>
        /// <param name="searchQuery">
        /// The search query.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public static IList<string> SplitWords(this string searchQuery)
        {
            IList<string> result = new List<string>();
            string[] tempResult = Regex.Split(searchQuery, Tag.MaxLengthFor.WordSplitting);
            var splitter = new Regex(Tag.MaxLengthFor.PunctuationSplitter, RegexOptions.Compiled);
            for (int i = 0; i < tempResult.Length; ++i)
            {
                string[] temp = splitter.Split(tempResult[i]);
                for (int j = 0; j < temp.Length; ++j)
                {
                    if (temp[j].Length >= Tag.MaxLengthFor.MinLength)
                    {
                        result.Add(temp[j].ToLowerInvariant());
                    }
                }
            }

            return result;
        }

        #endregion
    }
}