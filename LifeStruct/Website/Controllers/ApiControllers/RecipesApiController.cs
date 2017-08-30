using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using LifeStruct.Models;
using LifeStruct.Models.Account;
using LifeStruct.Models.Recipes;

namespace LifeStruct.Controllers.ApiControllers
{
    [Authorize]
    public class RecipesApiController : ApiController
    {
        DefaultConnection _db = new DefaultConnection();

        [Route("api/RecipesApi/AddRecipes")]
        [HttpPost]
        public IHttpActionResult AddRecipes(Request r)
        {
            if (string.IsNullOrEmpty(r.Rm.Id))
            {
                if (r.Im.Any())
                {
                    var recipeId = Guid.NewGuid().ToString();
                    foreach (var ingredient in r.Im)
                    {
                        if (!string.IsNullOrEmpty(ingredient.Amount) && !string.IsNullOrEmpty(ingredient.Calories) &&
                            !string.IsNullOrEmpty(ingredient.Name))
                        {
                            ingredient.Id = Guid.NewGuid().ToString();
                            ingredient.RecipesId = recipeId;
                            _db.Ingredients.Add(ingredient);
                        }
                        else
                        {
                            return BadRequest("Please fill out all fields");
                        }
                    }
                    _db.Recipes.Add(new RecipesModel
                    {
                        Description = r.Rm.Description,
                        Id = recipeId,
                        UserId = UserViewModel.GetCurrentUser().Id
                    });
                    _db.SaveChanges();
                    return Ok(recipeId);
                }
                return BadRequest("No ingredients");
            }
            var recipes = _db.Recipes.Find(r.Rm.Id);
            if (recipes != null && UserViewModel.GetCurrentUser().Id == recipes.UserId)
            {
                var findIngredient = new IngredientsModel();
                foreach (var i in r.Im)
                {
                    findIngredient = _db.Ingredients.Find(i.Id);
                    if (findIngredient != null)
                    {
                        findIngredient.Amount = i.Amount;
                        findIngredient.Calories = i.Calories;
                        findIngredient.FoodId = i.FoodId;
                        findIngredient.Name = i.Name;
                    }
                    else
                    {
                        _db.Ingredients.Add(i);
                    }
                }
                recipes.Description = r.Rm.Description;
                _db.Entry(findIngredient).State = EntityState.Modified;
                _db.SaveChanges();
                return Ok(r.Rm.Id);
            }
            return BadRequest();
        }
        [Route("api/RecipesApi/GetRecipes")]
        [HttpGet]
        public IHttpActionResult GetRecipes(string recipesId)
        {
            return Ok(new RecipesAndIngredients { IngredientsModelList = _db.Ingredients.Where(x => x.RecipesId == recipesId).ToList(), RecipesModel = _db.Recipes.FirstOrDefault(x => x.Id == recipesId) });
        }

    }

    public class Request
    {
        public List<IngredientsModel> Im { get; set; }
        public RecipesModel Rm { get; set; }
    }

    public class RecipesAndIngredients
    {
        public RecipesModel RecipesModel { get; set; }
        public List<IngredientsModel> IngredientsModelList { get; set; }
    }
}
