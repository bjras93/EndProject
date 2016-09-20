using LifeStruct.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LifeStruct.Controllers
{
    public class HomeController : Controller
    {
        DefaultConnection db = new DefaultConnection();
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                UserData ud = new UserData();
                var user = UserViewModel.GetCurrentUser();
                if (!string.IsNullOrEmpty(user.DietId))
                {
                    ud.Diet = db.Diet.Find(user.DietId);
                    ud.MealCollection = db.MealCollection.ToList().Where(x => x.DietId == ud.Diet.Id);
                    ud.Meals = db.Meals.ToList();
                    ud.Food = new List<Food>();
                    foreach (var m in ud.MealCollection)
                    {
                        var f = db.Food.Find(m.FoodId);
                        ud.Food.Add(new Food { Id = f.Id, Name = f.Name, Calories = f.Calories.ToString() });
                    }
                }
                if (!string.IsNullOrEmpty(user.FitnessId))
                {
                    ud.Fitness = db.Fitness.Find(user.FitnessId);
                }

                return View(ud);
            }
            return RedirectToAction("../Account/Index");
        }

    }
    public class HomeGet
    {
        public static FoodModel GetFoodById(string Id)
        {
            DefaultConnection db = new DefaultConnection();

            return db.Food.Find(Id);
        }
        public static IEnumerable<DietProgressModel> GetProgress(string UserId, string DietId, string FoodId, string Intake, int Meal)
        {
            DefaultConnection db = new DefaultConnection();
            return db.DietProgress.ToList().Where(x => x.UserId == UserId && x.DietId == DietId && x.FoodId == FoodId && x.CalorieIntake == Intake && x.Meal == Meal);

        }
    }
    public class UserData
    {
        public DietModel Diet { get; set; }
        public FitnessModel Fitness { get; set; }
        public IEnumerable<MealCollectionModel> MealCollection { get; set; }
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