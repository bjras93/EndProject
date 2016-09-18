namespace LifeStruct.Controllers.ApiControllers
{
    using Models;
    using System.Web.Http;

    public class UserApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [HttpGet]
        public ApplicationUser GetById(string Id)
        {                        
            return UserViewModel.GetUser(Id);
        }
    }
}
