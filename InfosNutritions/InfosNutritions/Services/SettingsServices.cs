using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfosNutritions.Entities;
using InfosNutritions.Helpers;
using InfosNutritions.Properties;
using Newtonsoft.Json;

namespace InfosNutritions.Services
{
    class SettingsServices : ISettingsServices
    {
        public List<FoodProduct> GetRecentProductsList()
        {
            string set = Settings.RecentSeachList;
            if (!string.IsNullOrEmpty(set))
            {
                var ret = JsonConvert.DeserializeObject<List<FoodProduct>>(Settings.RecentSeachList);
                return ret;
            }
            int capacity;
            if (int.TryParse(AppResources.RecentSeach_Capacity, out capacity))
            {
                return new List<FoodProduct>(capacity);
            }
            return new List<FoodProduct>(20);
        }

        public void AddProductToRecentList(FoodProduct e)
        {
            string set = Settings.RecentSeachList;
            if (!string.IsNullOrEmpty(set))
            {
                var recentList = JsonConvert.DeserializeObject<List<FoodProduct>>(set);
                if (recentList.Count == recentList.Capacity) recentList.RemoveAt(recentList.Count - 1);
                recentList.Insert(0, e);
                Settings.RecentSeachList = JsonConvert.SerializeObject(recentList);
            }
            else
            {
                List<FoodProduct> newList;
                newList = int.TryParse(AppResources.RecentSeach_Capacity, out int capacity) ?
                    new List<FoodProduct>(capacity) : new List<FoodProduct>(20);
                newList.Add(e);
                Settings.RecentSeachList = JsonConvert.SerializeObject(newList);
            }
        }
    }
}
