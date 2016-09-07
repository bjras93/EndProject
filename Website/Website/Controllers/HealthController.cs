using System.Web.Mvc;

namespace YouGo.Controllers
{
    public class HealthController : Controller
    {
        // GET: Health
        public ActionResult Index()
        {
            return View();
        }
    }
}