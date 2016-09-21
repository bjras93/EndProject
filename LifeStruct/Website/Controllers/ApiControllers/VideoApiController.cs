namespace Website.Controllers.ApiControllers
{
    using LifeStruct.Models;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Web.Http;
    public class VideoApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [Route("api/VideoApi/GetVideos")]
        [HttpPost]    
        public IHttpActionResult GetVideos(JObject jData)
        {
            dynamic json = jData;
            
            int take = Convert.ToInt32(json.take);
            int skip = Convert.ToInt32(json.skip);
            
            return Ok(db.Video.ToList().Skip(skip).Take(take));
        }
        [HttpGet]
        [Route("api/VideoApi/DeleteVideo")]
        public IHttpActionResult DeleteVideo(string Id, string UserId)
        {
            var user = UserViewModel.GetCurrentUser();
            if (user.Id == UserId)
            {
                db.Video.Remove(db.Video.Find(Id));
                db.SaveChanges();
                return Ok(db.Video.ToList());
            }
            return NotFound();
        }
    }
}
