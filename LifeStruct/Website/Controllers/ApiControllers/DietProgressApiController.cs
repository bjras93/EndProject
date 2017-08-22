using LifeStruct.Models.Account;
using LifeStruct.Models.Diet;
using LifeStruct.Models.Fitness;

namespace LifeStruct.Controllers.ApiControllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;
    using Newtonsoft.Json.Linq;

    public class DietProgressApiController : ApiController
    {
        DefaultConnection _db = new DefaultConnection();
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

            _db.DietProgress.Add(dp);
            _db.SaveChanges();
            return Ok(dp);
        }
        [Route("api/DietProgressApi/RemoveProgress")]
        [HttpGet]
        public IHttpActionResult RemoveProgress(string id)
        {
            DietProgressModel dpm = _db.DietProgress.Find(id);
            if (dpm != null)
            {
                decimal removed = Convert.ToDecimal(dpm.CalorieIntake);
                _db.DietProgress.Remove(dpm);

                _db.SaveChanges();

                return Ok(removed);
            }
            return NotFound();
        }
        [Route("api/DietProgressApi/GetProgress")]
        [HttpGet]
        public IHttpActionResult GetProgress()
        {
            var user = UserViewModel.GetCurrentUser();
            decimal height = Convert.ToDecimal(user.Height);
            decimal weight = Convert.ToDecimal(user.Weight);
            var age = DateTime.Now.Year - Convert.ToDateTime(user.Birthday).Year;
            double bmr = 0;
            DateTime max = new DateTime();
            var p = new Progress();
            
            if (user.Gender == 2)
            {
                bmr = ((10 * Convert.ToDouble(weight)) + (6.25 * Convert.ToDouble(height)) - (5 * age) + 5) *
                      Convert.ToDouble(user.ActivityLevel);
            }
            else
            {
                bmr = ((10 * Convert.ToDouble(weight)) + (6.25 * Convert.ToDouble(height)) - (5 * age) + -161) *
                      Convert.ToDouble(user.ActivityLevel);
            }
            foreach (var goal in _db.Goal.ToList().Where(x => x.UserId == user.Id))
            {
                if (DateTime.Parse(goal.Date) > max)
                {
                    p.Goal = goal;
                    max = Convert.ToDateTime(goal.Date);
                    if (goal.Goal == 1)
                    {
                        bmr = bmr * 1.10;
                    }
                    if (goal.Goal == 3)
                    {
                        bmr = bmr * 0.80;
                    }
                }
            }

            p.Bmr = Convert.ToDecimal(bmr);
            p.DietProgress =
                _db.DietProgress.ToList()
                    .Where(x => x.UserId == user.Id && x.Day == DateTime.Now.ToString());
            p.FitnessProgress =
                _db.FitnessProgress.ToList()
                    .Where(
                        x => x.UserId == user.Id && x.Date == DateTime.Now.ToString());

            return Ok(p);

        }
    }
    public class Progress
    {
        public IEnumerable<DietProgressModel> DietProgress { get; set; }
        public IEnumerable<FitnessProgressModel> FitnessProgress { get; set; }
        public GoalModel Goal { get; set; }
        public decimal Bmr { get; set; }
    }
}
