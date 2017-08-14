using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LifeStruct.Models.Account
{
    public class UserViewModel
    {
        public static ApplicationUser GetCurrentUser()
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());

            return user;
        }
        public static ApplicationUser GetUser(string id)
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);

            return user;
        }
    }
}