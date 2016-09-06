using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
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