using LifeStruct.Models;
using System;
using System.Web.Mvc;
using LifeStruct.Models.Account;
using LifeStruct.Models.Video;

namespace LifeStruct.Controllers
{
    public class VideosController : Controller
    {
        readonly DefaultConnection _db = new DefaultConnection();
        // GET: Video
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public ActionResult Create(VideoModel video)
        {
            if (Request.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    video.Id = Guid.NewGuid().ToString();
                    video.UserId = UserViewModel.GetCurrentUser().Id;
                    _db.Video.Add(video);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Videos");
                }
                if (!ModelState.IsValid)
                {

                    return View();
                }
            }
            return RedirectToAction("Index", "Account");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            VideoModel vm = _db.Video.Find(id);
            var user = UserViewModel.GetCurrentUser();
            if (vm != null && (Request.IsAuthenticated && user.Id == vm.UserId))
            {
                return View(vm);
            }
            return RedirectToAction("Index", "Account");
        }
        [HttpPost]
        public ActionResult Edit(VideoModel model)
        {
            var user = UserViewModel.GetCurrentUser();
            if (Request.IsAuthenticated && user.Id == model.UserId)
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Videos");
                }
                return View();
            }
            return RedirectToAction("Index", "Account");
        }
    }
}