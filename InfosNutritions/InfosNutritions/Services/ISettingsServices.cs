using System.Collections.Generic;
using InfosNutritions.Entities;

namespace InfosNutritions.Services
{
    public interface ISettingsServices
    {
        List<FoodProduct> GetRecentProductsList();
        void AddProductToRecentList(FoodProduct e);
    }
}