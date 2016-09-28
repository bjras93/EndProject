namespace LifeStruct.Controllers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web.Helpers;
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
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FitnessModel model)
        {

            if (Request.IsAuthenticated)
            {

                if (Request.Files.Count > 0)
                {
                    
                    var uploadFile = Request.Files[0];
                    if(uploadFile.ContentLength > (4096 * 1024))
                    {
                        ModelState.AddModelError("Filesize", "Image is too big, please select at smaller file");
                    }
                    if (uploadFile != null && uploadFile.ContentLength > 0 && uploadFile.ContentLength < (4096 * 1024))
                    {
                        var fullPath = Server.MapPath("~/Content/img/user/" + model.Img);
                        var sFullPath = Server.MapPath("~/Content/img/user/sm-" + model.Img);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                            System.IO.File.Delete(sFullPath);
                        }
                        var fileName = Path.GetFileName(uploadFile.FileName);
                        var fileId = Guid.NewGuid().ToString();
                        var path = Path.Combine(Server.MapPath("~/Content/img/user/"), fileId + fileName);
                        var sPath = Path.Combine(Server.MapPath("~/Content/img/user/"), "sm-" + fileId + fileName);
                        WebImage img = new WebImage(uploadFile.InputStream);
                        img.Save(path);
                        if (img.Width > 1000)
                        {
                            img.Resize(1000, 300, true);
                        }
                        model.Img = fileId + fileName;
                        img.Save(sPath);
                    }
                }
                model.Id = Guid.NewGuid().ToString();
                model.UserId = UserViewModel.GetCurrentUser().Id;
                model.Author = UserViewModel.GetCurrentUser().Name;
                if (ModelState.IsValid)
                {
                    db.Fitness.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("../Fitness/Edit/" + model.Id);
                }
                else
                {
                    return View(model);
                }
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
                FitnessView fv = new FitnessView();
                fv.Exercises = new List<ExerciseModel>();
                fv.Fitness = db.Fitness.Find(Id);
                fv.Schedule = db.Schedule.ToList().Where(x => x.FitnessId == fv.Fitness.Id);
                foreach (var exercise in fv.Schedule)
                {
                    fv.Exercises.Add(db.Exercise.Find(exercise.ExerciseId));
                }
                return View(fv);
            }
            return RedirectToAction("../Account/Index");
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(FitnessModel model)
        {
            var formData = Request.Form.AllKeys.ToList();
            ScheduleModel sm = new ScheduleModel();
            var fitnessId = Request.Form["Id"];
            var scheduleId = Request.Form["scheduleId"];
            var f = db.Fitness.Find(fitnessId);
            if (Request.Files.Count > 0)
            {
                var uploadFile = Request.Files[0];
                if(uploadFile.ContentLength > (4096 * 1024))
                {
                    ModelState.AddModelError("Filesize", "Image is too big, please try uploading a smaller one");
                }
                if (uploadFile != null && uploadFile.ContentLength > 0 && uploadFile.ContentLength < (4096 * 1024))
                {
                    var fullPath = Server.MapPath("~/Content/img/user/" + f.Img);
                    var sFullPath = Server.MapPath("~/Content/img/user/sm-" + f.Img);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        System.IO.File.Delete(sFullPath);
                    }
                    var fileName = Path.GetFileName(uploadFile.FileName);
                    var fileId = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/Content/img/user/"), fileId + fileName);
                    var sPath = Path.Combine(Server.MapPath("~/Content/img/user/"), "sm-" + fileId + fileName);
                    WebImage img = new WebImage(uploadFile.InputStream);
                    img.Save(path);
                    if (img.Width > 1000)
                    {
                        img.Resize(1000, 300, true);
                    }
                    f.Img = fileId + fileName;
                    img.Save(sPath);
                }
            }
            if (ModelState.IsValid)
            {
                f.Title = model.Title;
                f.Description = model.Description;
                f.Tags = model.Tags;
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                return View(model);
            }

            foreach (var key in formData)
            {
                if (!key.StartsWith("Description") && !key.StartsWith("Title"))
                {
                    string[] value = Request.Form[key].Split(',');
                    string[] values = key.Split('_');

                    if (value[0] != "")
                    {
                        if (key.StartsWith("schedule_"))
                        {
                            sm.Id = value[0];
                        }
                        if (key.StartsWith("calories_"))
                        {
                            sm.Calories = value[0];
                        }

                        if (key.StartsWith("exerciseId_"))
                        {                            
                            sm.ExerciseId = value[0];
                            sm.Exercise = db.Exercise.Find(sm.ExerciseId).Name;
                            int day = Convert.ToInt32(values[3].Replace("d", ""));
                            if (day == 7)
                            {
                                day = 0;
                            }
                            sm.Day = day;
                            sm.Week = Convert.ToInt32(values[4].Replace("w", ""));
                            sm.ExerciseIndex = Convert.ToInt32(values[2].Replace("e", ""));
                        }
                        if (key.StartsWith("interval_"))
                        {
                            sm.Time = value[0];
                        }
                        sm.FitnessId = fitnessId;
                        if (!string.IsNullOrEmpty(sm.Time))
                        {
                            if (string.IsNullOrEmpty(sm.Id))
                            {
                                sm.Id = Guid.NewGuid().ToString();
                                db.Schedule.Add(sm);
                                db.SaveChanges();
                            }
                            else
                            {
                                db.Entry(sm).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                            sm = new ScheduleModel();
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

    public class FitnessView
    {
        public List<ExerciseModel> Exercises { get; set; }
        public IEnumerable<ScheduleModel> Schedule { get; set; }
        public FitnessModel Fitness { get; set; }
    }
}