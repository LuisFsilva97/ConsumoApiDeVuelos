using Newshore.Viajes.Application.Exceptions;
using Newshore.Viajes.Application.IApplicationService;
using Newshore.Viajes.Business.IServices;
using Newshore.Viajes.Model.DTO;
using Newshore.Viajes.Model.Model;

namespace Newshore.Viajes.Application.ApplicationService
{
    public class SearchFlightApplicationService: ISearchFlightApplicationService
    {
        private readonly ISearchFlightService _searchFlightService;
        public SearchFlightApplicationService(ISearchFlightService searchFlightService) { 
            _searchFlightService = searchFlightService;
        }

        public async Task<Journey> SearchFlight(SearchDto request)
        {
            validateRequestData(request);
            var foundFlights = new Journey();
            
            try
            {
                foundFlights = await _searchFlightService.SearchFlight(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            validateFoundFlights(foundFlights);

            return foundFlights;
        }

        private void validateRequestData(SearchDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Origin) || string.IsNullOrWhiteSpace(request.Destination)) {
                throw new BadRequestException("Debe ingresar un origen y destino validos");
            }
        }

        private void validateFoundFlights(Journey foundFlights)
        {
            if (!foundFlights.Flights.Any())
            {
                throw new NotFoundException("BuscarRutas", "No fue posible calcular la ruta con los parametros de busqueda ingresados");
            }
        }

        public async Task<IEnumerable<SearchHistory>> GetHistory()
        {
            var historySearch = new List<SearchHistory>();
            try
            {
                var result = await _searchFlightService.GetHistory();
                historySearch = result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return historySearch;
        }
    }
}
