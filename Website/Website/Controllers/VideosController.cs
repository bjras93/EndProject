using System.Web.Mvc;

namespace YouGo.Controllers
{
    public class VideosController : Controller
    {
        // GET: Video
        public ActionResult Index()
        {
            return View();
        }
    }
}