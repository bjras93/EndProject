using System.Web.Mvc;

namespace YouGo.Controllers
{
    public class DietsController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int ID)
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddNewFood()
        {

        return PartialView();
        }
    }
}