using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LifeStruct.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        // GET: Recipes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
        [HttpGet]
        public ActionResult _AddNewFood()
        {

            return PartialView("~/Views/Shared/_AddNewFood.cshtml");
        }
    }
}