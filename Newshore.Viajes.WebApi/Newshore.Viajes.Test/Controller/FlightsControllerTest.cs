using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newshore.Viajes.Api.Controllers;
using Newshore.Viajes.Application.IApplicationService;
using Newshore.Viajes.Business.IServices;
using Newshore.Viajes.Communications.IServices;
using Newshore.Viajes.Communications.Services;
using Newshore.Viajes.Model.DTO;
using Newshore.Viajes.Model.Model;
using Newshore.Viajes.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.Test.Controller
{
    [TestCategory("UnitTest")]
    [TestClass]
    public class FlightsControllerTest: BaseTest
    {
        FlightsController _mutantController;
        private readonly Mock<ILogger<FlightsController>> _fakeLogger;
        private readonly Mock<IApiFlightsService> _fakeApiFlightsService;
        private ISearchFlightApplicationService _searchFlightApplicationService;

        public FlightsControllerTest() {
            _fakeLogger = new Mock<ILogger<FlightsController>>();
            _fakeApiFlightsService = new Mock<IApiFlightsService>();
        }

        [TestInitialize]
        public void Initialize()
        {
            _searchFlightApplicationService = serviceProvider.GetRequiredService<ISearchFlightApplicationService>();

            _fakeApiFlightsService.Setup(f => f.GetFlights()).Returns(Task.FromResult(GetFakeDataApiService()));

            servicesCollection.AddSingleton(_fakeApiFlightsService.Object);

            serviceProvider = servicesCollection.BuildServiceProvider();


            _mutantController = ActivatorUtilities.CreateInstance<FlightsController>(serviceProvider, _fakeLogger.Object);
        }

        [TestMethod]
        public async Task SearchTestSucces()
        {
            SearchDto request = new SearchDto()
            {
                Origin = "MZL",
                Destination = "BCN"
            };

            var result = await _mutantController.Search(request);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Flights.Count() == 2);

            var searchHistory = _searchFlightApplicationService.GetHistory();

            Assert.IsNotNull(searchHistory);
            Assert.IsTrue(searchHistory.Result.Count() == 1);
        }

        [TestMethod]
        public async Task SearchTestBadRequestError()
        {
            SearchDto request = new SearchDto()
            {
                Origin = "MZLX",
                Destination = "BCNX"
            };
            try
            {
                var result = await _mutantController.Search(request);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.IsTrue(ex.Message.Equals("BuscarRutas -- No fue posible calcular la ruta con los parametros de busqueda ingresados"));
            }
        }

        [TestMethod]
        public async Task SearchTestNoFundError()
        {
            SearchDto request = new SearchDto()
            {
                Origin = "",
                Destination = ""
            };
            try
            {
                var result = await _mutantController.Search(request);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
                Assert.IsTrue(ex.Message.Equals("Debe ingresar un origen y destino validos"));
            }
        }
    }
}
