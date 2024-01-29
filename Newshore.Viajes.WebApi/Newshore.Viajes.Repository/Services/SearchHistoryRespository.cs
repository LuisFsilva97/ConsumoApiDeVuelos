using Newshore.Viajes.Model.Model;
using Newshore.Viajes.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Repository.Services
{
    public class SearchHistoryRespository : ISearchHistoryRespository
    {
        private readonly IGenericRepository<SearchHistory> _searchHistoryRepository;

        public SearchHistoryRespository(IGenericRepository<SearchHistory> searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        public async Task<SearchHistory> Save(SearchHistory searchHistory)
        {
            await _searchHistoryRepository.Add(searchHistory);

            return searchHistory;
        }

        public async Task<IEnumerable<SearchHistory>> GetAll()
        {
            return await _searchHistoryRepository.GetList();
        }
    }
}
