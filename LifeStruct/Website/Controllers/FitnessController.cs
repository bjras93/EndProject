namespace LifeStruct.Controllers
{
    using Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    public class FitnessController : Controller
    {
        DefaultConnection db = new DefaultConnection();
        // GET: Fitness
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
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
        [HttpPost]
        public ActionResult Create(string f_title, string f_description)
        {

            if (Request.IsAuthenticated)
            {
                FitnessModel fm = new FitnessModel();
                fm.Id = Guid.NewGuid().ToString();
                fm.Title = f_title;
                fm.Description = f_description;

                db.Fitness.Add(fm);
                db.SaveChanges();
                return RedirectToAction("../Fitness/Edit/" + fm.Id);
            }
            return RedirectToAction("../Account/Index");
        }
        public ActionResult Edit(string Id)
        {
            if(Request.IsAuthenticated)
            {
                var s = db.Schedule.ToList().Where(x => x.FitnessId == Id);
                var days = db.Days.ToList();

                if(s.Any())
                {
                    ViewBag.Schedule = s;
                }
                ViewBag.Days = days;
                return View();
            }
            return RedirectToAction("../Account/Index");
        }

        [HttpGet]
        public ActionResult _AddNewExercise()
        {

            return PartialView("~/Views/Shared/_AddNewExercise.cshtml");
        }
    }
}