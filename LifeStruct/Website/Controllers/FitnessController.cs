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
                return View(db.Fitness);
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
                fm.UserId = UserViewModel.GetCurrentUser().Id;

                db.Fitness.Add(fm);
                db.SaveChanges();
                return RedirectToAction("../Fitness/Edit/" + fm.Id);
            }
            return RedirectToAction("../Account/Index");
        }
        public ActionResult Edit(string Id)
        {
            if (Request.IsAuthenticated)
            {

                if (db.Fitness.Find(Id).UserId == UserViewModel.GetCurrentUser().Id)
                {
                    var s = db.Schedule.ToList().Where(x => x.FitnessId == Id);
                    var days = db.Days.ToList();

                    if (s.Any())
                    {
                        ViewBag.Schedule = s;
                    }
                    ViewBag.Days = days;
                    return View(db.Fitness.Find(Id));
                }
                else
                {
                    return RedirectToAction("../Fitness/Details/" + Id);

                }
            }
            return RedirectToAction("../Account/Index");
        }
        public ActionResult Details(string Id)
        {
            if (Request.IsAuthenticated)
            {
                var s = db.Fitness.Find(Id);
                return View(s);
            }
            return RedirectToAction("../Account/Index");
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Edit()
        {
            var formData = Request.Form.AllKeys.ToList();

            ScheduleModel sm = new ScheduleModel();
            var fitnessId = Request.Form["Id"];
            var count = 0;
            foreach (var key in formData)
            {

                string[] value = Request.Form[key].Split(',');
                string[] values = key.Split('_');

                if (key.StartsWith("s_"))
                {
                    if (value[0] != "")
                    {
                        if (key.StartsWith("s_exerciseId"))
                        {
                            sm.ExerciseId = value[0];
                            count++;
                        }
                        if (key.StartsWith("s_day"))
                        {
                            sm.Day = value[0];
                            count++;
                        }
                        if (key.StartsWith("s_interval"))
                        {
                            sm.Time = value[0];
                            count++;
                        }
                        if (key.StartsWith("s_id"))
                        {
                            sm.Id = value[0];
                        }
                        if (count == 3)
                        {
                            sm.FitnessId = fitnessId;

                            if (string.IsNullOrEmpty(sm.Id))
                            {
                                sm.Id = Guid.NewGuid().ToString();
                                db.Schedule.Add(sm);
                                db.SaveChanges();
                            }
                            else
                            {
                                db.Entry(sm).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            sm = new ScheduleModel();
                            count = 0;
                        }
                    }


                }
            }
            return RedirectToAction("../Fitness/Details/" + fitnessId);

        }

        [HttpGet]
        public ActionResult _AddNewExercise()
        {

            return PartialView("~/Views/Shared/_AddNewExercise.cshtml");
        }
    }
}