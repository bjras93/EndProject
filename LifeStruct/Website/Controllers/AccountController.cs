using LifeStruct.Models.Account;

namespace LifeStruct.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Models;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        readonly DefaultConnection _db = new DefaultConnection();
        public AccountController()
        {

        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Details()
        {
            if (Request.IsAuthenticated)
            {
                var ud = new UserDetail
                {
                    User = UserViewModel.GetCurrentUser(),
                    Activity = _db.Activity
                };
                DateTime dt = new DateTime();
                foreach (var g in _db.Goal.Where(x => x.UserId == ud.User.Id))
                {
                    if (Convert.ToDateTime(g.Date).Date > dt.Date)
                    {
                        dt = Convert.ToDateTime(g.Date);
                  }
                }
                ud.Goal = _db.Goal.ToList().First(x => x.UserId == ud.User.Id && x.Date == dt.ToString("dd-MM-yyyy"));
                return View(ud);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Details(UserDetail model)
        {
            if (Request.IsAuthenticated)
            {
                var user = UserViewModel.GetUser(model.User.Id);
                WeightModel wm = new WeightModel();
                DateTime dt = new DateTime();
                foreach (var g in _db.Goal.Where(x => x.UserId == user.Id))
                {
                    if (Convert.ToDateTime(g.Date).Date > dt.Date)
                    {
                        dt = Convert.ToDateTime(g.Date);
                    }
                }
                if (!_db.Weight.ToList().Any(x => x.UserId == user.Id && x.Date == DateTime.Now.ToString("dd-MM-yyyy")))
                {
                    wm.Id = Guid.NewGuid().ToString();
                    wm.UserId = user.Id;
                    wm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                    wm.Weight = Convert.ToDecimal(model.User.Weight);
                    _db.Weight.Add(wm);
                    _db.SaveChanges();
                }
                model.Goal = _db.Goal.ToList().First(x => x.UserId == user.Id && x.Date == dt.ToString("dd-MM-yyyy"));
                model.Activity = _db.Activity;
                user.Weight = model.User.Weight;
                user.ActiveLevel = model.User.ActiveLevel;
                user.Name = model.User.Name;
                UserManager.Update(user);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        
        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Index");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);

            var google = loginInfo.ExternalIdentity.Name;
            if (google != null)
            {
                if (loginInfo.ExternalIdentity.Name.IndexOf("(", StringComparison.Ordinal) > 0)
                {
                    google = loginInfo.ExternalIdentity.Name.Substring(0, loginInfo.ExternalIdentity.Name.IndexOf("(", StringComparison.Ordinal));
                }
            }
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel
                    {
                        Email = loginInfo.Email,

                        Name = google
                    });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("../Account/Index");
                }

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Name = model.Name, Height = model.Height, Weight = model.Weight, DietId = "", DietDate = "", FitnessId = "", FitnessDate = "", Birthday = model.Birthday, Gender = model.Gender };

                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, false, false);
                        return RedirectToLocal(returnUrl);
                    }

                    WeightModel wm = new WeightModel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = user.Id,
                        Date = DateTime.Now.ToString("dd-MM-yyyy"),
                        Weight = Convert.ToDecimal(user.Weight)
                    };
                    _db.Weight.Add(wm);
                    _db.SaveChanges();
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Account");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion


    }

    public class UserDetail
    {
        public IEnumerable<ActivityModel> Activity { get; set; }
        public IEnumerable<SelectListItem> ActivityDropdown => new SelectList(Activity, "Multiplier", "Name");
        public ApplicationUser User { get; set; }
        public GoalModel Goal { get; set; }
    }
}