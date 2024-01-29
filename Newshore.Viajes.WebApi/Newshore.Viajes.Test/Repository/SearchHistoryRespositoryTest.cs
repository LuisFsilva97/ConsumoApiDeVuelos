using Microsoft.Extensions.DependencyInjection;
using Newshore.Viajes.Model.Model;
using Newshore.Viajes.Repository.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Viajes.UnitTest.Repository
{
    [TestCategory("UnitTest")]
    [TestClass]
    public class SearchHistoryRespositoryTest: BaseTest
    {
        private ISearchHistoryRespository _searchHistoryRespository;

        [TestInitialize]
        public void Initialize()
        {
            _searchHistoryRespository = serviceProvider.GetRequiredService<ISearchHistoryRespository>();
        }

        [TestMethod]
        public async Task searchHistorySaveAndGetAllTest()
        {
            var initialData = await _searchHistoryRespository.GetAll();
            SearchHistory searchHistory = new SearchHistory()
            {
                Id = 1,
                Origin = "MZL",
                Destination = "MDE",
                SearchDate = DateTime.Now
            };
            var result = await _searchHistoryRespository.Save(searchHistory);

            Assert.IsNotNull(result);

            var finalData = await _searchHistoryRespository.GetAll();

            Assert.IsTrue(initialData.Count() < finalData.Count());
        }
    }
}
