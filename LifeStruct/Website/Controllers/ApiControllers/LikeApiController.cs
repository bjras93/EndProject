namespace LifeStruct.Controllers.ApiControllers
{
    using Models;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Web.Http;

    public class LikeApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [Route("api/LikeApi/Like")]
        [HttpPost]
        public IHttpActionResult Like(JObject jsonData)
        {
            var lm = new LikeModel();

            dynamic json = jsonData;
            var user = UserViewModel.GetCurrentUser();
            var liked = db.Like.ToList().Where(x => x.Type == Convert.ToInt32(json.type) && x.TypeId == json.typeId.ToString() && x.UserId == user.Id);
            if (liked.Count() == 0)
            {
                lm.Id = Guid.NewGuid().ToString();
                lm.Type = json.type;
                lm.UserId = user.Id;
                lm.TypeId = json.typeId.ToString();
                db.Like.Add(lm);
                if (json.type == 1)
                {
                    DietModel d = db.Diet.Find(json.typeId.ToString());
                    d.Likes++;
                    db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                }
                if(json.type == 2)
                {
                    FitnessModel f = db.Fitness.Find(json.typeId.ToString());
                    f.Likes++;
                    db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                }
                if(json.type == 3)
                {
                    HealthModel h = db.Health.Find(json.typeId.ToString());
                    h.Likes++;
                    db.Entry(h).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return Ok(lm);
            }
            else
            {
                var like = liked.First();

                db.Like.Remove(like);
                if (json.type == 1)
                {
                    DietModel d = db.Diet.Find(json.typeId.ToString());
                    d.Likes = d.Likes -1;
                    db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                }
                if (json.type == 2)
                {
                    FitnessModel f = db.Fitness.Find(json.typeId.ToString());
                    f.Likes = f.Likes -1;
                    db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                }
                if (json.type == 3)
                {
                    HealthModel h = db.Health.Find(json.typeId.ToString());
                    h.Likes = h.Likes - 1;
                    db.Entry(h).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return Ok();
            }          

        }

        [Route("api/LikeApi/FindByUIdType")]
        [HttpGet]
        public IHttpActionResult FindByUIdType(string uId, int type)
        {
            var like = db.Like.ToList().Where(x => x.Type == type && x.UserId == uId);

            return Ok(like);
        }
    }
}
