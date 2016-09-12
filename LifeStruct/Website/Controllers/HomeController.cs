using System.Web.Mvc;

namespace LifeStruct.Controllers
{
    public class HomeController : Controller
    {
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