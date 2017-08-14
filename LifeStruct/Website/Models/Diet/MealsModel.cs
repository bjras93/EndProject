using System.Collections.Generic;

namespace LifeStruct.Models.Diet
{
    public class MealsModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FoodModel> Food { get;set;}
    }
}