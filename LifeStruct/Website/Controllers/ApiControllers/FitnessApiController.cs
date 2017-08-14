using System;
using System.Linq;
using System.Web.Http;
using LifeStruct.Models;
using LifeStruct.Models.Account;
using LifeStruct.Models.Fitness;
using Newtonsoft.Json.Linq;

namespace LifeStruct.Controllers.ApiControllers
{
    public class FitnessApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [HttpGet]
        [Route("api/FitnessApi/GetFitness")]
        public IHttpActionResult GetFitness()
        {
            return Ok(db.Fitness.ToList());
        }
        [HttpPost]
        [Route("api/FitnessApi/SetProgress")]
        public IHttpActionResult SetProgress(JObject jsonData)
        {
            dynamic json = jsonData;
            FitnessProgressModel fpm = new FitnessProgressModel();
            fpm.Id = Guid.NewGuid().ToString();
            fpm.UserId = UserViewModel.GetCurrentUser().Id;
            fpm.FitnessId = json.fitnessId.ToString();
            fpm.ExerciseId = json.exerciseId;
            fpm.Loss = json.loss;
            fpm.Date = DateTime.Now.ToString("dd-MM-yyyy");

            db.FitnessProgress.Add(fpm);
            db.SaveChanges();
            return Ok(fpm);
        }
        [HttpGet]
        [Route("api/FitnessApi/RemoveProgress")]
        public IHttpActionResult RemoveProgress(string Id)
        {
            var remove = db.FitnessProgress.Find(Id).Loss;
            db.FitnessProgress.Remove(db.FitnessProgress.Find(Id));
            db.SaveChanges();
            return Ok(remove);
        }
        }
}
