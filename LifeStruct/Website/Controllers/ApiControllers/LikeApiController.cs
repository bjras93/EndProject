using LifeStruct.Models.Account;
using LifeStruct.Models.Diet;
using LifeStruct.Models.Fitness;
using LifeStruct.Models.Health;

namespace LifeStruct.Controllers.ApiControllers
{
    using Models;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Web.Http;

    public class LikeApiController : ApiController
    {
        DefaultConnection _db = new DefaultConnection();
        [Route("api/LikeApi/Like")]
        [HttpPost]
        public IHttpActionResult Like(JObject jsonData)
        {
            var lm = new LikeModel();

            dynamic json = jsonData;
            var user = UserViewModel.GetCurrentUser();
            var liked = _db.Like.ToList().Where(x => x.Type == Convert.ToInt32(json.type) && x.TypeId == json.typeId.ToString() && x.UserId == user.Id);
            var likeModels = liked as LikeModel[] ?? liked.ToArray();
            if (!likeModels.Any())
            {
                lm.Id = Guid.NewGuid().ToString();
                lm.Type = json.type;
                lm.UserId = user.Id;
                lm.TypeId = json.typeId.ToString();
                _db.Like.Add(lm);
                if (json.type == 1)
                {
                    DietModel d = _db.Diet.Find(json.typeId.ToString());
                    d.Likes++;
                    _db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                }
                if(json.type == 2)
                {
                    FitnessModel f = _db.Fitness.Find(json.typeId.ToString());
                    f.Likes++;
                    _db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                }
                if(json.type == 3)
                {
                    HealthModel h = _db.Health.Find(json.typeId.ToString());
                    h.Likes++;
                    _db.Entry(h).State = System.Data.Entity.EntityState.Modified;
                }
                _db.SaveChanges();
                return Ok(lm);
            }
            else
            {
                var like = likeModels.First();

                _db.Like.Remove(like);
                if (json.type == 1)
                {
                    DietModel d = _db.Diet.Find(json.typeId.ToString());
                    d.Likes = d.Likes -1;
                    _db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                }
                if (json.type == 2)
                {
                    FitnessModel f = _db.Fitness.Find(json.typeId.ToString());
                    f.Likes = f.Likes -1;
                    _db.Entry(f).State = System.Data.Entity.EntityState.Modified;
                }
                if (json.type == 3)
                {
                    HealthModel h = _db.Health.Find(json.typeId.ToString());
                    h.Likes = h.Likes - 1;
                    _db.Entry(h).State = System.Data.Entity.EntityState.Modified;
                }
                _db.SaveChanges();
                return Ok();
            }          

        }

        [Route("api/LikeApi/FindByUIdType")]
        [HttpGet]
        public IHttpActionResult FindByUIdType(string uId, int type)
        {
            var like = _db.Like.ToList().Where(x => x.Type == type && x.UserId == uId);

            return Ok(like);
        }
    }
}
