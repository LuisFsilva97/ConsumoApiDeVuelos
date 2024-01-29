using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newshore.Viajes.Communications.IServices;
using Newshore.Viajes.Model.DTO;
using System.IO;
using System.Net.Http.Json;

namespace Newshore.Viajes.Communications.Services
{
    public class ApiFlightsService: IApiFlightsService
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        public ApiFlightsService(IConfiguration configuration, IMemoryCache memoryCache) {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }
 
        public async Task<List<FlightResponseDto>> GetFlights() {
            List<FlightResponseDto> flights = new List<FlightResponseDto>();
            HttpClient client = new HttpClient();

            int level = 0;
            int.TryParse(_configuration["CommunicationServices:ApiFlightsLevel"],out level);
                
            string path = string.Format(_configuration["CommunicationServices:ApiFlights" ?? ""], level);

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                flights = await response.Content.ReadFromJsonAsync<List<FlightResponseDto>>();
            }

            return flights;
        }
    }
}