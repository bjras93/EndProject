using LifeStruct.Models.Account;
using LifeStruct.Models.Health;

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
        public IHttpActionResult GetArticles(int type)
        {
            HealthInformation health = new HealthInformation();

            health.Articles = db.Health.ToList().Where(x => x.Type == type);
            health.Users = new List<Users>();
            foreach (var article in health.Articles)
            {
                health.Users.Add(new Users { UserId = article.UserId, Name = UserViewModel.GetUser(article.UserId).Name, ArticleId = article.Id });
            }
            return Ok(health);
        }
        [Route("api/HealthApi/DeleteArticle")]
        [HttpGet]
        public IHttpActionResult DeleteArticle(string id, string userId)
        {
            var user = UserViewModel.GetCurrentUser();
            if (user.Id == userId)
            {
                db.Health.Remove(db.Health.Find(id));
                db.SaveChanges();
                return Ok();

            }
            else
            {
                return NotFound();
            }
        }

    }
    public class HealthInformation
    {

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
