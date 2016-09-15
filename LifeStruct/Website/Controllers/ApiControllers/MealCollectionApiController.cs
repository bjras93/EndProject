namespace Website.Controllers.ApiControllers
{
    using LifeStruct.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    public class MealCollectionApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [Route("api/MealCollectionApi/FindByDietId")]
        [HttpGet]
        public IEnumerable<MealCollectionModel> FindByDietId(string Id)
        {
            var result = db.MealCollection.Where(x => x.DietId == Id);

            return result;
        }
    }
}
