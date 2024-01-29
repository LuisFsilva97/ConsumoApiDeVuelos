using Newshore.Viajes.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Repository.IServices
{
    public interface ISearchHistoryRespository
    {
        Task<SearchHistory> Save(SearchHistory searchHistory);

        Task<IEnumerable<SearchHistory>> GetAll();
    }
}
