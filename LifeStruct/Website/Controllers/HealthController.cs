using System.Web.Mvc;

namespace LifeStruct.Controllers
{
    public class HealthController : Controller
    {
        // GET: Health
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("../Account/Index");
        }
    }
}