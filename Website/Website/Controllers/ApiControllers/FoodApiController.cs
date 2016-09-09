using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YouGo.Models;

namespace YouGo.Controllers.ApiControllers
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

    }
}
