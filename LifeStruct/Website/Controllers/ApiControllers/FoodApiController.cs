using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LifeStruct.Models;
using LifeStruct.Models.Diet;

namespace LifeStruct.Controllers.ApiControllers
{
    public class FoodApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();

        public IEnumerable<FoodModel> GetFoods()
        {
            return db.Food.ToList();
        }
        public IHttpActionResult GetFood(string Id)
        {
            var food = db.Food.Find(Id);

            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }
        [HttpPost]
        public IHttpActionResult AddFood(JObject jData)
        {
            dynamic json = jData;
            string name = json.Name;
            string calories = json.Calories;
            FoodModel fm = new FoodModel();
            fm.Id = Guid.NewGuid().ToString();
            fm.Calories = int.Parse(calories);
            fm.Name = name;

            db.Food.Add(fm);
            db.SaveChanges();
            return Ok();
        }

    }
}
