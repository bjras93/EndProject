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
    }
}
