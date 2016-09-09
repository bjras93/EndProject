namespace YouGo.Controllers.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;
    public class MealsApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();

        public IEnumerable<MealsModel> GetMeals()
        {
            return db.Meals.ToList();
        }
    }
}
