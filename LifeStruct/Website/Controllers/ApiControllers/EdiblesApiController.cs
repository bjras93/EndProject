using LifeStruct.Models.Diet;

namespace LifeStruct.Controllers.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;
    public class EdiblesApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        public IEnumerable<FoodModel> GetAll()
        {
            return db.Food.ToList();
        }
        [HttpGet]
        public IEnumerable<FoodModel> FindByName(string s)
        {
            var result = from c in db.Food where c.Name.ToLower().StartsWith(s.ToLower()) select c;

            return result.ToList();
        }        
    }
}
