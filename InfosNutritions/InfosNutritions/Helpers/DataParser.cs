using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfosNutritions.Entities;
using Newtonsoft.Json.Linq;

namespace InfosNutritions.Helpers
{
    public static class DataParser
    {
        public static FoodProduct ToFoodProduct(JObject jo)
        {
            if ((string)jo["status_verbose"] == "product found")
            {
                FoodProduct r = new FoodProduct
                {
                    Name = (string)jo["product"]["product_name"],
                    ImgUrl = (string)jo["image_front_url"],
                    Serving = (string)jo["product"]["nutrition_data_per"],
                    Energy = int.Parse((string)jo["product"]["nutriments"]["energy"])
                };
                return r;
            }
            return null;
        }
    }
}
