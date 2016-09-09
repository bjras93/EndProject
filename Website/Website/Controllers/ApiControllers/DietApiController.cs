using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YouGo.Models;

namespace YouGo.Controllers.ApiControllers
{
    public class DietApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();

        public IEnumerable<DietModel> GetAll()
        {
            return db.Diet.ToList();
        }
        public  IHttpActionResult GetDiet(string Id)
        {
            var diet = db.Diet.Find(Id);

            if (diet == null)
            {
                return NotFound();
            }

            return Ok(diet);
        }

        [HttpPost]
        public IHttpActionResult PostDiet(JObject jsonData)
        {
            var dm = new DietModel();

            dynamic json = jsonData;
            var user = UserViewModel.GetCurrentUser();

            dm.Id = Guid.NewGuid().ToString();
            dm.User = user.Id;
            dm.Title = json.title.ToString();
            dm.Description = json.description.ToString();
            if (json.img != null)
            {
                dm.Img = json.img.ToString();
            }
            else
            {
                dm.Img = "";
            }

            db.Diet.Add(dm);
            db.SaveChanges();
            return Ok(dm.Id);
        }
    }
}
