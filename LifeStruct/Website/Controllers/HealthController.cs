namespace LifeStruct.Controllers
{
    using Models.Account;
    using Models.Health;
    using Models;
    using System;
    using System.Web.Mvc;

    public class HealthController : Controller
    {
        readonly DefaultConnection _db = new DefaultConnection();
        // GET: Health
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
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
        public ActionResult Create(HealthModel model)
        {
            if (Request.IsAuthenticated)
            {
                model.Id = Guid.NewGuid().ToString();
                model.UserId = UserViewModel.GetCurrentUser().Id;
                if(string.IsNullOrEmpty(model.Tags))
                {
                    model.Tags = "";
                }
                if (ModelState.IsValid)
                {                    
                    _db.Health.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("../Health/Details/" + model.Id);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Edit(string id)
        {
            if (Request.IsAuthenticated)
            {
                return View(_db.Health.Find(id));
            }
            return RedirectToAction("Index", "Account");
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(HealthModel model)
        {
            if (Request.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(model.Tags))
                {
                    model.Tags = "";
                }
                if (ModelState.IsValid)
                {

                    _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    
                    _db.SaveChanges();
                    return RedirectToAction("../Health/Details/" + model.Id);
                }
                return View(model);
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Details(string id)
        {
            if (Request.IsAuthenticated)
            {
                return View(_db.Health.Find(id));
            }
            return RedirectToAction("Index", "Account");
        }
    }
}