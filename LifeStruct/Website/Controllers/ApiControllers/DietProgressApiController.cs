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

            DateTime dt = new DateTime();
            foreach (var g in db.Goal.Where(x => x.UserId == user.Id))
            {
                if (Convert.ToDateTime(g.Date).Date > dt.Date)
                {
                    dt = Convert.ToDateTime(g.Date);
                }
            }

            Progress p = new Progress();
            p.Goal = db.Goal.ToList().Where(x => x.UserId == user.Id && x.Date == dt.ToString("dd-MM-yyyy")).First();
            decimal height = Convert.ToDecimal(user.Height);
            decimal weight = Convert.ToDecimal(user.Weight);
            decimal heightDivided = height / 100;
            decimal bmi = weight / (heightDivided * heightDivided);
            var age = DateTime.Now.Year - Convert.ToDateTime(user.Birthday).Year;
            double bmr = 0;
            if (user.Gender == 2)
            {
                bmr = ((10 * Convert.ToDouble(weight)) + (6.25 * Convert.ToDouble(height)) - (5 * age) + 5) * Convert.ToDouble(user.ActiveLevel);
            }
            else
            {
                bmr = ((10 * Convert.ToDouble(weight)) + (6.25 * Convert.ToDouble(height)) - (5 * age) + -161) * Convert.ToDouble(user.ActiveLevel);

            }

            if (p.Goal.Goal == 1)
            {
                bmr = bmr * 1.10;
            }
            if (p.Goal.Goal == 3)
            {
                bmr = bmr * 0.80;
            }
            p.Bmr = Convert.ToDecimal(bmr);
            p.DietProgress = db.DietProgress.ToList().Where(x => x.UserId == user.Id && Convert.ToDateTime(x.Day).DayOfWeek == DateTime.Now.DayOfWeek);
            p.FitnessProgress = db.FitnessProgress.ToList().Where(x => x.UserId == user.Id && Convert.ToDateTime(x.Date).DayOfWeek == DateTime.Now.DayOfWeek);
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
