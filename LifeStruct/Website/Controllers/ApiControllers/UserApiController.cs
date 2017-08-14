using System.Globalization;
using LifeStruct.Models.Account;

namespace LifeStruct.Controllers.ApiControllers
{
    using Microsoft.AspNet.Identity;
    using Models;
    using Newtonsoft.Json.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using System.Web.Http;
    using System;
    public class UserApiController : ApiController
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public UserApiController()
        {

        }
        public UserApiController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        DefaultConnection db = new DefaultConnection();
        [System.Web.Mvc.Route("api/UserApi/GetById")]
        [System.Web.Mvc.HttpGet]
        public ApplicationUser GetById(string Id)
        {                        
            return UserViewModel.GetUser(Id);
        }
        [System.Web.Mvc.Route("api/UserApi/SetDiet")]
        [System.Web.Mvc.HttpPost]
        public async Task<IHttpActionResult> SetDiet(JObject jsonData)
        {
            dynamic json = jsonData;
            ApplicationUser user = UserViewModel.GetUser(json.uId.ToString());
            IdentityResult result;
            if (json.type == 1)
            {
                if (json.dId != null && !Convert.ToBoolean(json.add))
                {
                    user.DietId = json.dId.ToString();
                    user.DietDate = DateTime.Now.ToString();
                    result = await UserManager.UpdateAsync(user);
                }
                else
                {
                    user.DietId = "";
                    user.DietDate = "";
                    result = await UserManager.UpdateAsync(user);
                }                
            }
            else
            {

                if (json.fId != null && !Convert.ToBoolean(json.add))
                {
                    user.FitnessId = json.fId.ToString();
                    user.FitnessDate = DateTime.Now.ToString();
                    result = await UserManager.UpdateAsync(user);
                }
                else
                {
                    user.FitnessId = "";
                    user.FitnessDate = "";
                    result = await UserManager.UpdateAsync(user);
                }
            }
            if (result.Succeeded)
            {
                db.SaveChanges();
            }

            return Ok(user);
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
