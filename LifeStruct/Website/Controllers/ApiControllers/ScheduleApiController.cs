namespace LifeStruct.Controllers.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;
    public class ScheduleApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [Route("api/ScheduleApi/FindById")]
        [HttpGet]
        public IEnumerable<ScheduleModel> FindById(string Id)
        {
            var result = db.Schedule.Where(x => x.FitnessId == Id);

            return result;
        }
    }
}
