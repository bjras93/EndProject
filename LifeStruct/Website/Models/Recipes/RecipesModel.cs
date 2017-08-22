using System.ComponentModel.DataAnnotations;

namespace LifeStruct.Models.Recipes
{
    public class RecipesModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "You need to write something")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You need to write something")]
        public string Calories { get; set; }
        [Required(ErrorMessage = "You need to write something")]
        public string Amount { get; set; }
    }
}