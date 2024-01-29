using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newshore.Viajes.Communications.IServices;
using Newshore.Viajes.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Communications.Services
{
    public class CachedApiFlightsService: IApiFlightsService
    {
        private const string FlightsListCacheKey = "FlightsList";
        private readonly IMemoryCache _memoryCache;
        private readonly IApiFlightsService  _apiFlightsService;
        private readonly IConfiguration _configuration;

        public CachedApiFlightsService( IApiFlightsService apiFlightsService,  IMemoryCache memoryCache, IConfiguration configuration)
        {
            _apiFlightsService = apiFlightsService;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public async Task<List<FlightResponseDto>> GetFlights()
        {
            var cacheOptions = getConfigCacheOptions();

            if (_memoryCache.TryGetValue(FlightsListCacheKey, out List<FlightResponseDto> query))
                return query;

            query = await _apiFlightsService.GetFlights();

            _memoryCache.Set(FlightsListCacheKey, query, cacheOptions);

            return query;

        }

        private MemoryCacheEntryOptions getConfigCacheOptions()
        {
            double slidingExpiration = 0;
            double aAbsoluteExpiration = 0;
            double.TryParse(_configuration["MemoryCacheConfig:SetSlidingExpirationFromMinutes"] ?? "10", out slidingExpiration);
            double.TryParse(_configuration["MemoryCacheConfig:SetAbsoluteExpirationFromMinutes"] ?? "60", out aAbsoluteExpiration);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpiration))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(aAbsoluteExpiration));

            return cacheOptions;
        }
    }
}
