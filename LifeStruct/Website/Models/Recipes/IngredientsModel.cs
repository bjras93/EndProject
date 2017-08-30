using System.ComponentModel.DataAnnotations;

namespace LifeStruct.Models.Recipes
{
    public class IngredientsModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "You need to fill out ingredient")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You need to pick or create an ingredient")]
        public string Calories { get; set; }
        public string Amount { get; set; }
        public string FoodId { get; set; }
        public string RecipesId { get; set; }
    }
}