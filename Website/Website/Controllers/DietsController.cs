using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YouGo.Models;

namespace YouGo.Controllers
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
        public ActionResult Create(string id)
        {

            if (Request.IsAuthenticated)
            {
                return View(db.Diet.Find(id));
            }
            return RedirectToAction("../Account/Index");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create()
        {
            var formData = Request.Form.AllKeys.ToList();

            MealCollectionModel mcm = new MealCollectionModel();
            var dietId = Request.Form["Id"];

            foreach (var key in formData)
            {

                string[] value = Request.Form[key].Split(',');
                string[] values = key.Split('_');

                if (key.StartsWith("name_") || key.StartsWith("amount_") || key.StartsWith("foodId_"))
                {
                    if (value[0] != "")
                    {

                        if (key.StartsWith("name_"))
                        {
                            int week = int.Parse(values[1].Substring(1));
                            int meal = int.Parse(values[2].Substring(1));
                            int day = int.Parse(values[3].Substring(1));
                            int edible = int.Parse(values[4].Substring(1));

                            mcm.Id = Guid.NewGuid().ToString();
                            mcm.WeekNo = week;
                            mcm.Meal = meal;
                            mcm.DietId = dietId;
                            mcm.Edible = edible;

                        }
                        if (key.StartsWith("amount_"))
                        {

                            mcm.Amount = value[0];


                        }
                        if (key.StartsWith("foodId_"))
                        {

                            mcm.FoodId = value[1];
                        }
                        if (!string.IsNullOrEmpty(mcm.FoodId) && !string.IsNullOrEmpty(mcm.Amount))
                        {
                            //if (!string.IsNullOrEmpty(dietId))
                            //{
                            //    db.Entry(mcm).State = System.Data.Entity.EntityState.Modified;
                            //    db.SaveChanges();
                            //}


                            db.MealCollection.Add(mcm);
                            db.SaveChanges();

                        }
                    }

                    
                }
                
            }
            return View();
        }

        public ActionResult Details(string ID)
        {
            if (Request.IsAuthenticated)
            {
                DietModel dm = new DietModel();
                dm = db.Diet.Find(ID);
                return View(dm);
            }
            return RedirectToAction("../Account/Index");
        }


        public ActionResult Edit(int ID)
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("../Account/Index");
        }
        [HttpGet]
        public ActionResult AddNewFood()
        {

            return PartialView();
        }
    }
}