using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InfosNutritions.Helpers;
using InfosNutritions.Properties;

namespace InfosNutritions.Providers
{
    class DataProvider : IDataProvider
    {
        #region Fields

        private readonly MyHttpClient _httpClient;

        #endregion

        #region Constructor

        public DataProvider()
        {
            _httpClient = new MyHttpClient(AppResources.OpenFoodFacts_BaseURI,AppResources.OpenFoodFacts_URIExtension);
        }

        #endregion
        public async Task<string> RequestProductInfo(string input)
        {
            return await _httpClient.RequestWithResult(HttpMethod.Get, input);
        }
    }
}
