namespace Website.Controllers.ApiControllers
{
    using LifeStruct.Models;
    using System.Linq;
    using System.Web.Http;
    public class VideoApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();        
        public IHttpActionResult GetVideos()
        {
            return Ok(db.Video.ToList());
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
