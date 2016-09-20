using LifeStruct.Models;
using System;
using System.Web.Mvc;

namespace LifeStruct.Controllers
{
    public class VideosController : Controller
    {
        DefaultConnection db = new DefaultConnection();
        // GET: Video
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
        [HttpPost]
        public ActionResult Create(VideoModel Video)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    Video.Id = Guid.NewGuid().ToString();
                    Video.UserId = UserViewModel.GetCurrentUser().Id;
                    db.Video.Add(Video);
                    db.SaveChanges();
                    return RedirectToAction("../Videos/Index");
                }
                if (!ModelState.IsValid)
                {

                    return View();
                }
            }
            return RedirectToAction("../Account/Index");
        }
    }
}