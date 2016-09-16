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


        [Route("api/MealCollectionApi/GetCollection")]
        [HttpGet]
        public Diet GetCollection(string dietId)
        {
            var d = new Diet();
            d.MealCollection = db.MealCollection.Where(x => x.DietId == dietId);
            d.Food = new List<string>();

            foreach(var mc in d.MealCollection)
            {
                var fDb = db.Food.Find(mc.FoodId);
                d.Food.Add(fDb.Name + "," + fDb.Calories);
            }
            return d;
           
        }
    }

    public class Diet
    {
        public IEnumerable<MealCollectionModel> MealCollection { get; set; }
        public List<string> Food { get; set; }
    }

}
