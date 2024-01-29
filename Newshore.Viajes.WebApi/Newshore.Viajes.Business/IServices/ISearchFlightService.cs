using Newshore.Viajes.Model.DTO;
using Newshore.Viajes.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Business.IServices
{
    public interface ISearchFlightService
    {
        Task<Journey> SearchFlight(SearchDto request);

        Task<IEnumerable<SearchHistory>> GetHistory();
    }
}
