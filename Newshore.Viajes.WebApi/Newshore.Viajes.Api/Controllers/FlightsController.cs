using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newshore.Viajes.Application.Exceptions;
using Newshore.Viajes.Application.IApplicationService;
using Newshore.Viajes.Communications.IServices;
using Newshore.Viajes.Model.DTO;
using Newshore.Viajes.Model.Model;

namespace Newshore.Viajes.Api.Controllers
{
    [Route("api/flights")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly ILogger<FlightsController> _logger;
        private readonly ISearchFlightApplicationService _searchFlightApplicationService;

        public FlightsController(ILogger<FlightsController> logger, ISearchFlightApplicationService searchFlightApplicationService)
        {
            _logger = logger;
            _searchFlightApplicationService = searchFlightApplicationService;
        }

        /// <summary>
        /// Buscar rutas de vuelos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Name = "SearchRoute")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelException), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ModelException), StatusCodes.Status404NotFound)]
        public async Task<Journey> Search(SearchDto request)
        {
            return await _searchFlightApplicationService.SearchFlight(request);
        }

        /// <summary>
        /// Consultar listado de busquedas realizadas
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "HistoriesSearch")]
        public async Task<IEnumerable<SearchHistory>> Histories()
        {
            return await _searchFlightApplicationService.GetHistory();
        }
    }
}
