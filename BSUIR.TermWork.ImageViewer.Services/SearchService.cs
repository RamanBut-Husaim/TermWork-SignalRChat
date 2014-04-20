using System;
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
    public sealed class SearchService : ServiceBase, ISearchService
    {
        public SearchService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

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

        private bool ValidateSearchQuery(string searchQuery)
        {
            bool result =
                !(string.IsNullOrEmpty(searchQuery)
                  || searchQuery.Length < Tag.MaxLengthFor.MinLength);
            return result;
        }
    }
}
