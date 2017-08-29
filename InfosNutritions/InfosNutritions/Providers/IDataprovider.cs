using System.Threading.Tasks;

namespace InfosNutritions.Providers
{
    public interface IDataProvider
    {
        Task<string> RequestProductInfo(string input);
    }
}