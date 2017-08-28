using System.Collections.Generic;

namespace InfosNutritions.Services
{
    public interface ISettingsServices
    {
        List<FoodProduct> GetRecentProductsList();
        void AddProductToRecentList(FoodProduct e);
    }
}