namespace LifeStruct.Controllers.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;
    using System;
    using Newtonsoft.Json.Linq;
    public class ExerciseApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [HttpGet]
        public IEnumerable<ExerciseModel> FindByName(string s)
        {
            var result = from c in db.Exercise where c.Name.ToLower().StartsWith(s.ToLower()) select c;

            return result;
        }
        [HttpPost]
        public IHttpActionResult AddFood(JObject jData)
        {
            dynamic json = jData;
            string name = json.Name;
            string calories = json.Calories;
            ExerciseModel em = new ExerciseModel();
            em.Id = Guid.NewGuid().ToString();
            em.Name = name;
            em.Calories = int.Parse(calories);

            db.Exercise.Add(em);
            db.SaveChanges();
            return Ok();
        }

    }
}
