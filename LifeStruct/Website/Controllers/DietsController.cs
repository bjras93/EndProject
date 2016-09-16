using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LifeStruct.Models;
using System.Data.Entity;

namespace LifeStruct.Controllers
{
    public class DietsController : Controller
    {
        DefaultConnection db = new DefaultConnection();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View(db.Diet);
            }
            return RedirectToAction("../Account/Index");
        }
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {

                return View();
            }

            return RedirectToAction("../Account/Index");
        }
        public ActionResult Edit(string id)
        {
            if (Request.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(id) && db.Diet.Find(id) != null)
                {
                    return View(db.Diet.Find(id));
                }
                else
                {
                    return RedirectToAction("../Diets/Create");
                }
            }

            return RedirectToAction("../Account/Index");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreateDiet()
        {
            var formData = Request.Form.AllKeys.ToList();

            MealCollectionModel mcm = new MealCollectionModel();
            var dietId = Request.Form["Id"];

            foreach (var key in formData)
            {

                string[] value = Request.Form[key].Split(',');
                string[] values = key.Split('_');

                if (key.StartsWith("name_") || key.StartsWith("amount_") || key.StartsWith("foodId_") || key.StartsWith("collectionId_"))
                {
                    if (value[0] != "")
                    {

                        if (key.StartsWith("name_"))
                        {
                            int week = int.Parse(values[1].Substring(1));
                            int meal = int.Parse(values[2].Substring(1));
                            int day = int.Parse(values[3].Substring(1));
                            int edible = int.Parse(values[4].Substring(1));

                            mcm.WeekNo = week;
                            mcm.Meal = meal;
                            mcm.Day = day;
                            mcm.DietId = dietId;
                            mcm.Edible = edible;

                        }
                        if (key.StartsWith("amount_"))
                        {
                            mcm.Amount = value[0];
                        }
                        if (key.StartsWith("foodId_"))
                        {
                            mcm.FoodId = value[0];
                        }
                        if (key.StartsWith("collectionId_"))
                        {
                            mcm.Id = value[0];
                        }
                        if (!string.IsNullOrEmpty(mcm.FoodId) && !string.IsNullOrEmpty(mcm.Amount))
                        {
                            if (string.IsNullOrEmpty(mcm.Id))
                            {
                                mcm.Id = Guid.NewGuid().ToString();
                                db.MealCollection.Add(mcm);
                                db.SaveChanges();

                            }
                            else
                            {
                                db.Entry(mcm).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            mcm = new MealCollectionModel();
                        }
                    }


                }

            }
            return RedirectToAction("../Diets/Details/" + dietId);
        }

        public ActionResult Details(string ID)
        {
            if (Request.IsAuthenticated)
            {
                DietView dv = new DietView();
                dv.MealCollection = new List<MealCollectionView>();
                var dDb = db.Diet.Find(ID);
                dv.Title = dDb.Title;
                dv.Description = dDb.Description;
                dv.Meals = db.Meals;
                dv.Days = db.Days;
                foreach (var mcm in db.MealCollection.ToList().Where(x => x.DietId == ID))
                {
                    var fDb = db.Food.Find(mcm.FoodId);
                    dv.MealCollection.Add(new MealCollectionView { Amount = mcm.Amount, Day = mcm.Day, Food = fDb.Name, Calories = fDb.Calories, Meal = mcm.Meal, Week = mcm.WeekNo });
                }
                foreach(var dMcm in dv.MealCollection)
                {

                }
                
                return View(dv);
            }
            return RedirectToAction("../Account/Index");
        }

        [HttpGet]
        public ActionResult _AddNewFood()
        {

            return PartialView("~/Views/Shared/_AddNewFood.cshtml");
        }
    }
    public class DietView {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<MealCollectionView> MealCollection { get; set; }        
        public DbSet<DaysModel> Days { get; set; }
        public DbSet<MealsModel> Meals { get; set; }
    }
    public class MealCollectionView
    {
        public int Day { get; set; }
        public int Meal { get; set; }
        public string Food { get; set; }
        public int Calories { get; set; }
        public int Week { get; set; }
        public string Amount { get; set; }

    }
}