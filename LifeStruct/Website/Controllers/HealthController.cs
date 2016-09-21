namespace LifeStruct.Controllers
{
    using Models;
    using System;
    using System.Web.Mvc;

    public class HealthController : Controller
    {
        DefaultConnection db = new DefaultConnection();
        // GET: Health
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
                    db.Health.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("../Health/Details/" + model.Id);
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("../Account/Index");
        }
        public ActionResult Edit()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("../Account/Index");
        }
        public ActionResult Details(string Id)
        {
            if (Request.IsAuthenticated)
            {
                return View(db.Health.Find(Id));
            }
            return RedirectToAction("../Account/Index");
        }
    }
}