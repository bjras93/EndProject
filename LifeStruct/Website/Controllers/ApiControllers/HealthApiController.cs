namespace LifeStruct.Controllers.ApiControllers
{
    using LifeStruct.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    public class HealthApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [Route("api/HealthApi/GetArticles")]
        [HttpGet]
        public IHttpActionResult GetArticles()
        {
            HealthInformation health = new HealthInformation();

            health.Articles = db.Health.ToList();
            health.Users = new List<Users>();
            foreach(var article in health.Articles)
            {
                health.Users.Add(new Users { UserId = article.UserId, Name = UserViewModel.GetUser(article.UserId).Name, ArticleId = article.Id });
            }
            return Ok(health);
        }


    }
    public class HealthInformation {

        public IEnumerable<HealthModel> Articles { get; set; }
        public List<Users> Users { get; set; }
    }
    public class Users
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ArticleId { get; set; }
    }

}
