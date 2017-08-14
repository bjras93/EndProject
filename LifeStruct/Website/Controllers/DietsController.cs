using LifeStruct.Models.Account;
using LifeStruct.Models.Diet;

namespace LifeStruct.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using System.Data.Entity;
    using System.IO;
    using System.Web.Helpers;
    public class DietsController : Controller
    {
        readonly DefaultConnection _db = new DefaultConnection();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View(_db.Diet);
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {

                return View();
            }

            return RedirectToAction("Index", "Account");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(DietModel dm)
        {
            if (Request.IsAuthenticated)
            {
                var user = UserViewModel.GetCurrentUser();
                if (ModelState.IsValid)
                {
                    dm.Id = Guid.NewGuid().ToString();
                    dm.Author = user.Name;
                    dm.User = user.Id;
                    dm.Likes = 0;
                    if (Request.Files.Count > 0)
                    {
                        var uploadFile = Request.Files[0];
                        if (uploadFile != null && uploadFile.ContentLength > 0 && uploadFile.ContentLength < (4096 * 1024))
                        {
                            var fileName = Path.GetFileName(uploadFile.FileName);
                            var fileId = Guid.NewGuid().ToString();
                            var path = Path.Combine(Server.MapPath("~/Content/img/user/"), fileId + fileName);
                            var sPath = Path.Combine(Server.MapPath("~/Content/img/user/"), "sm-" + fileId + fileName);
                            WebImage img = new WebImage(uploadFile.InputStream);
                            img.Save(path);
                            if (img.Width > 1000)
                            {
                                img.Resize(1000, 300);
                            }
                            dm.Img = fileId + fileName;

                            img.Save(sPath);
                        }
                        if (uploadFile != null && uploadFile.ContentLength > (4096 * 1024))
                        {
                            ModelState.AddModelError("Filesize", "Filesize is too big please select a smaller image");
                        }
                    }
                    else
                    {
                        dm.Img = "";
                    }
                    _db.Diet.Add(dm);
                    _db.SaveChanges();
                    return RedirectToAction("../Diets/Edit/" + dm.Id);
                }
                return View(dm);
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Edit(string id)
        {
            if (Request.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(id) && _db.Diet.Find(id) != null)
                {
                    return View(_db.Diet.Find(id));
                }
                return RedirectToAction("Create", "Diets");
            }
            return RedirectToAction("Index", "Account");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(DietModel dm)
        {
            var formData = Request.Form.AllKeys.ToList();

            MealCollectionModel mcm = new MealCollectionModel();

            var dietId = Request.Form["Id"];
            var d = _db.Diet.Find(dietId);
            if (Request.Files.Count > 0)
            {
                var uploadFile = Request.Files[0];

                if (uploadFile != null && uploadFile.ContentLength > (4096 * 1024))
                {
                    ModelState.AddModelError("Filesize", "Filesize is too big please try uploading another image");

                }
                if (uploadFile != null && (uploadFile.ContentLength > 0 && uploadFile.ContentLength < (4096 * 1024)))
                {
                    if (d != null)
                    {
                        var fullPath = Server.MapPath("~/Content/img/user/" + d.Img);
                        var sFullPath = Server.MapPath("~/Content/img/user/sm-" + d.Img);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                            System.IO.File.Delete(sFullPath);
                        }
                    }
                    var fileName = Path.GetFileName(uploadFile.FileName);
                    var fileId = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/Content/img/user/"), fileId + fileName);
                    var sPath = Path.Combine(Server.MapPath("~/Content/img/user/"), "sm-" + fileId + fileName);
                    WebImage img = new WebImage(uploadFile.InputStream);
                    img.Save(path);
                    if (img.Width > 1000)
                    {
                        img.Resize(1000, 300);
                    }
                    if (d != null) d.Img = fileId + fileName;
                    img.Save(sPath);
                }
            }
            if (ModelState.IsValid)
            {
                if (d != null)
                {
                    d.Title = dm.Title;
                    d.Description = dm.Description;
                    d.Tags = dm.Tags;
                    _db.Entry(d).State = EntityState.Modified;
                }
                _db.SaveChanges();
            }
            else
            {
                return View(dm);
            }
            foreach (var key in formData)
            {
                if (!key.StartsWith("Description"))
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

                                day = day - 1;
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
                                    _db.MealCollection.Add(mcm);
                                    _db.SaveChanges();

                                }
                                else
                                {
                                    _db.Entry(mcm).State = EntityState.Modified;
                                    _db.SaveChanges();
                                }
                                mcm = new MealCollectionModel();
                            }
                        }
                    }

                }

            }
            return RedirectToAction("../Diets/Details/" + dietId);
        }

        public ActionResult Details(string id)
        {
            if (Request.IsAuthenticated)
            {
                DietView dv = new DietView {MealCollection = new List<MealCollectionView>()};
                var dDb = _db.Diet.Find(id);
                if (dDb != null)
                {
                    dv.Title = dDb.Title;
                    dv.Description = dDb.Description;
                    dv.Meals = _db.Meals;
                    dv.Days = _db.Days;
                    dv.Img = dDb.Img;
                }

                foreach (var mcm in _db.MealCollection.ToList().Where(x => x.DietId == id))
                {
                    int value = mcm.WeekNo;
                    if (value > dv.Weeks)
                    {
                        dv.Weeks = value;
                    }
                    var fDb = _db.Food.Find(mcm.FoodId);
                    if (fDb != null)
                        dv.MealCollection.Add(new MealCollectionView { Amount = mcm.Amount, Day = mcm.Day, Food = fDb.Name, Calories = fDb.Calories, Meal = mcm.Meal, Week = mcm.WeekNo });
                }

                return View(dv);
            }
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public ActionResult _AddNewFood()
        {

            return PartialView("~/Views/Shared/_AddNewFood.cshtml");
        }
    }
    public class DietView
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public List<MealCollectionView> MealCollection { get; set; }
        public int Weeks { get; set; }
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