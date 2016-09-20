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

            db.DietProgress.Add(dp);
            db.SaveChanges();
            return Ok(dp);
        }
        [Route("api/DietProgressApi/RemoveProgress")]
        [HttpGet]
        public IHttpActionResult RemoveProgress(string Id)
        {
            DietProgressModel dpm = db.DietProgress.Find(Id);

            db.DietProgress.Remove(dpm);

            db.SaveChanges();

            return Ok();
        }
    }
}
