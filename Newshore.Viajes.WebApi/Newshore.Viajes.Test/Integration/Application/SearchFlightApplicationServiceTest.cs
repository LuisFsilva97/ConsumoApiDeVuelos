using Microsoft.Extensions.DependencyInjection;
using Newshore.Viajes.Application.ApplicationService;
using Newshore.Viajes.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.UnitTest.Integration.Application
{
    [TestCategory("Integration")]
    [TestClass]
    public class SearchFlightApplicationServiceTest: BaseTest
    {
        [TestMethod]
        public async Task SearchFlightTetSuccess()
        {
            SearchDto request = new SearchDto()
            {
                Origin = "MZL",
                Destination = "BCN"
            };
            var classToTest = ActivatorUtilities.CreateInstance<SearchFlightApplicationService>(serviceProvider);
            var result = await classToTest.SearchFlight(request);

            Assert.IsNotNull(result);
        }
    }
}
