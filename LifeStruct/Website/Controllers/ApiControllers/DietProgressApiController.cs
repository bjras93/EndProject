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
            dp.Day = DateTime.Now.ToString("dd-MM-yyyy");
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


            decimal height = Convert.ToDecimal(user.Height);
            decimal weight = Convert.ToDecimal(user.Weight);
            var age = DateTime.Now.Year - Convert.ToDateTime(user.Birthday).Year;
            double bmr = 0;
            var dt = DateTime.Now.ToString("dd-MM-yyyy");
            DateTime max = new DateTime();
            var p = new Progress();
            foreach (var goal in db.Goal.ToList().Where(x => x.UserId == user.Id))
            {
                if (Convert.ToDateTime(goal.Date) > max)
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


            if (user.Gender == 2)
            {
                bmr = ((10 * Convert.ToDouble(weight)) + (6.25 * Convert.ToDouble(height)) - (5 * age) + 5) *
                      Convert.ToDouble(user.ActiveLevel);
            }
            else
            {
                bmr = ((10 * Convert.ToDouble(weight)) + (6.25 * Convert.ToDouble(height)) - (5 * age) + -161) *
                      Convert.ToDouble(user.ActiveLevel);

            }


            p.Bmr = Convert.ToDecimal(bmr);
            p.DietProgress =
                db.DietProgress.ToList()
                    .Where(x => x.UserId == user.Id && x.Day == DateTime.Now.ToString("dd-MM-yyyy"));
            p.FitnessProgress =
                db.FitnessProgress.ToList()
                    .Where(
                        x => x.UserId == user.Id && x.Date == DateTime.Now.ToString("dd-MM-yyyy"));

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
