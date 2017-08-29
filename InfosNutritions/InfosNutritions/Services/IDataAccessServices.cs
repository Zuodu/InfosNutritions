using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace InfosNutritions.Services
{
    public interface IDataAccessServices
    {
        Task<JObject> GetProductInfo(string code);
    }
}