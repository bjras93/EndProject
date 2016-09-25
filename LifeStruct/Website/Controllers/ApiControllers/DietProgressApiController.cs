namespace LifeStruct.Website.Controllers.ApiControllers
{
    using Newtonsoft.Json.Linq;
    using System.Web.Http;
    using Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public class DietProgressApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [Route("api/DietProgressApi/AddProgress")]
        [HttpPost]
        public IHttpActionResult AddProgress(JObject jsonData)
        {
            dynamic json = jsonData;
            DietProgressModel dp = new DietProgressModel();

            dp.Id = Guid.NewGuid().ToString();
            dp.UserId = json.userId.ToString();
            dp.DietId = json.dietId.ToString();
            dp.CalorieIntake = json.intake.ToString();
            dp.FoodId = json.foodId.ToString();
            dp.Day = DateTime.Now.ToString();
            dp.Meal = Convert.ToInt32(json.meal);

            db.DietProgress.Add(dp);
            db.SaveChanges();
            return Ok(dp);
        }
        [Route("api/DietProgressApi/RemoveProgress")]
        [HttpGet]
        public IHttpActionResult RemoveProgress(string Id)
        {
            DietProgressModel dpm = db.DietProgress.Find(Id);
            decimal removed = Convert.ToDecimal(dpm.CalorieIntake);
            db.DietProgress.Remove(dpm);

            db.SaveChanges();

            return Ok(removed);
        }
        [Route("api/DietProgressApi/GetProgress")]
        [HttpGet]
        public IHttpActionResult GetProgress()
        {
            var user = UserViewModel.GetCurrentUser();
            Progress p = new Progress();
            p.DietProgress = db.DietProgress.ToList().Where(x => x.UserId == user.Id && Convert.ToDateTime(x.Day).DayOfWeek == DateTime.Now.DayOfWeek);
            p.FitnessProgress = db.FitnessProgress.ToList().Where(x => x.UserId == user.Id && Convert.ToDateTime(x.Date).DayOfWeek == DateTime.Now.DayOfWeek);
            return Ok(p);
        }
    }
    public class Progress
    {
        public IEnumerable<DietProgressModel> DietProgress { get; set; }
        public IEnumerable<FitnessProgressModel> FitnessProgress { get; set; }
    }
}
