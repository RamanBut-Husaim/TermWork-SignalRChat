﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchService.cs" company="">
//   
// </copyright>
// <summary>
//   The search service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections.Generic;
using System.Linq;

using BSUIR.TermWork.ImageViewer.Data;
using BSUIR.TermWork.ImageViewer.Data.Exceptions;
using BSUIR.TermWork.ImageViewer.Model;
using BSUIR.TermWork.ImageViewer.Services.Contracts;
using BSUIR.TermWork.ImageViewer.Services.Contracts.Exceptions;
using BSUIR.TermWork.ImageViewer.Services.Extensions;

namespace BSUIR.TermWork.ImageViewer.Services
{
    /// <summary>
    /// The search service.
    /// </summary>
    public sealed class SearchService : ServiceBase, ISearchService
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchService"/> class.
        /// </summary>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public SearchService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The search albums.
        /// </summary>
        /// <param name="searchQuery">
        /// The search query.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="SearchServiceException">
        /// </exception>
        public IList<Album> SearchAlbums(string searchQuery)
        {
            IEnumerable<Album> result = new List<Album>();
            if (!this.ValidateSearchQuery(searchQuery))
            {
                return result.ToList();
            }

            try
            {
                IList<string> tagWords = searchQuery.SplitWords();
                tagWords = tagWords.RemoveDuplicates();
                result =
                    result.Concat(
                        this.UnitOfWork.Repository<Album, int>()
                            .Query()
                            .Include(p => p.Tags)
                            .Filter(
                                p =>
                                p.Tags.Any(
                                    s =>
                                    tagWords.Any(
                                        f => f.Contains(s.Content) || s.Content.Contains(f))))
                            .Get()
                            .ToList());
            }
            catch (DbException ex)
            {
                throw new SearchServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result.ToList();
        }

        /// <summary>
        /// The search images.
        /// </summary>
        /// <param name="searchQuery">
        /// The search query.
        /// </param>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        /// <exception cref="SearchServiceException">
        /// </exception>
        public IList<Image> SearchImages(string searchQuery)
        {
            IEnumerable<Image> result = new List<Image>();
            if (!this.ValidateSearchQuery(searchQuery))
            {
                return result.ToList();
            }

            try
            {
                IList<string> tagWords = searchQuery.SplitWords();
                tagWords = tagWords.RemoveDuplicates();
                result =
                    result.Concat(
                        this.UnitOfWork.Repository<Image, int>()
                            .Query()
                            .Include(p => p.Tags)
                            .Filter(
                                p =>
                                p.Tags.Any(
                                    s =>
                                    tagWords.Any(
                                        f => f.Contains(s.Content) || s.Content.Contains(f))))
                            .Get()
                            .ToList());
            }
            catch (DbException ex)
            {
                throw new SearchServiceException(EntityValidationException.DefaultMessage, ex);
            }

            return result.ToList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The validate search query.
        /// </summary>
        /// <param name="searchQuery">
        /// The search query.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ValidateSearchQuery(string searchQuery)
        {
            bool result =
                !(string.IsNullOrEmpty(searchQuery)
                  || searchQuery.Length < Tag.MaxLengthFor.MinLength);
            return result;
        }

        #endregion
    }
}