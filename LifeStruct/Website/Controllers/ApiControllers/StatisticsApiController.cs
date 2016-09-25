namespace LifeStruct
{
    using Models;
    using System;
    using System.Linq;
    using System.Web.Http;
    public class StatisticsApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [Route("api/StatisticsApi/GetData")]
        [HttpGet]
        public IHttpActionResult GetData()
        {
            var user = UserViewModel.GetCurrentUser();
            var data = db.Weight.ToList().Where(x => x.UserId == user.Id);
            return Ok(data);
        }
        [Route("api/StatisticsApi/GetMood")]
        [HttpGet]
        public IHttpActionResult GetMood()
        {
            var user = UserViewModel.GetCurrentUser();
            var data = db.Mood.ToList().Where(x => x.UserId.Split('_')[0] == user.Id);
            return Ok(data);
        }
    }
}
