using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfosNutritions.Entities
{
    public class FoodProduct
    {
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public int Energy { get; set; }
        public string Serving { get; set; }
    }
}
