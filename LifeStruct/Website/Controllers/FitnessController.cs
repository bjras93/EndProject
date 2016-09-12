using System.Web.Mvc;

namespace LifeStruct.Controllers
{
    public class FitnessController : Controller
    {
        // GET: Fitness
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