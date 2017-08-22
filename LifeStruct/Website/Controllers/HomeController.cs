namespace LifeStruct.Controllers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Models.Account;
    using Models.Diet;
    using Models.Fitness;
    public class HomeController : Controller
    {
        DefaultConnection _db = new DefaultConnection();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                UserData ud = new UserData();
                var user = UserViewModel.GetCurrentUser();
                if (!string.IsNullOrEmpty(user.DietId))
                {
                    ud.Diet = _db.Diet.Find(user.DietId);
                    ud.MealCollection = _db.MealCollection.ToList().Where(x => x.DietId == ud.Diet.Id);
                    ud.Meals = _db.Meals.ToList();
                    ud.Food = new List<Food>();
                    foreach (var m in ud.MealCollection)
                    {
                        var f = _db.Food.Find(m.FoodId);
                        if (f != null)
                            ud.Food.Add(new Food { Id = f.Id, Name = f.Name, Calories = f.Calories.ToString() });
                    }
                }
                if (!string.IsNullOrEmpty(user.FitnessId))
                {
                    ud.Exercises = new List<ExerciseModel>();
                    ud.Fitness = _db.Fitness.Find(user.FitnessId);
                    ud.Schedule = _db.Schedule.Where(x => x.FitnessId == user.FitnessId);
                    ud.FitnessProgress = _db.FitnessProgress.Where(x => x.FitnessId == ud.Fitness.Id && x.UserId == user.Id);

                    foreach (var exercise in ud.Schedule)
                    {
                        ud.Exercises.Add(_db.Exercise.Find(exercise.ExerciseId));
                    }

                }

                return View(ud);
            }
            return RedirectToAction("Index", "Account");
        }

    }
    public class HomeGet
    {
        public static FoodModel GetFoodById(string id)
        {
            DefaultConnection db = new DefaultConnection();

            return db.Food.Find(id);
        }
        public static IEnumerable<DietProgressModel> GetProgress(string userId, string dietId, string foodId, string intake, int meal)
        {
            DefaultConnection db = new DefaultConnection();
            return db.DietProgress.ToList().Where(x => x.UserId == userId && x.DietId == dietId && x.FoodId == foodId && x.CalorieIntake == intake && x.Meal == meal && Convert.ToDateTime(x.Day).ToString() == DateTime.Now.ToString());

        }
    }
    public class UserData
    {
        public DietModel Diet { get; set; }
        public FitnessModel Fitness { get; set; }
        public IEnumerable<ScheduleModel> Schedule { get; set; }
        public List<ExerciseModel> Exercises { get; set; }
        public IEnumerable<MealCollectionModel> MealCollection { get; set; }
        public IEnumerable<FitnessProgressModel> FitnessProgress { get; set; }
        public IEnumerable<MealsModel> Meals { get; set; }
        public List<Food> Food { get; set; }
    }
    public class Food
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Calories { get; set; }
    }
}