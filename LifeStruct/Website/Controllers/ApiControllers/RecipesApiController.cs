using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using LifeStruct.Models;
using LifeStruct.Models.Recipes;

namespace LifeStruct.Controllers.ApiControllers
{
    public class RecipesApiController : ApiController
    {
        DefaultConnection _db = new DefaultConnection();
        [HttpPost]
        public JsonResult<List<RecipesModel>> AddRecipes(List<RecipesModel> m)
        {
            if (ModelState.IsValid)
            {
                foreach (var recipe in m)
                {
                    recipe.Id = Guid.NewGuid().ToString();
                    _db.Recipes.Add(recipe);
                }
                _db.SaveChanges();
            }
            return Json(m);
        }
    }
}
