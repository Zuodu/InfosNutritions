using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfosNutritions.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InfosNutritions.Services
{
    class DataAccessServices : IDataAccessServices
    {
        #region fields

        private readonly IDataProvider _dataProvider;

        #endregion

        #region Constructor

        public DataAccessServices(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #endregion
        public async Task<JObject> GetProductInfo(string code)
        {
            var raw = await _dataProvider.RequestProductInfo(code);
            JObject jo = JObject.Parse(raw);
            JObject jo2 = JsonConvert.DeserializeObject<JObject>(raw);
            return jo;
        }
    }
}
