using System.Web.Mvc;

namespace LifeStruct.Controllers
{
    public class VideosController : Controller
    {
        // GET: Video
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