namespace YouGo.Models
{
    using System.Collections.Generic;
    public class MealsModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FoodModel> Food { get;set;}
    }
}