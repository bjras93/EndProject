namespace YouGo.Controllers.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;

    public class DaysApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();

        public IEnumerable<DaysModel> GetDays()
        {
            return db.Days.ToList();
        }
    }
}
