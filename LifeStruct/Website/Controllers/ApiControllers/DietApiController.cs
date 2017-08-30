using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using LifeStruct.Models;
using LifeStruct.Models.Account;
using LifeStruct.Models.Diet;

namespace LifeStruct.Controllers.ApiControllers
{
    [Authorize]
    public class DietApiController : ApiController
    {
        DefaultConnection _db = new DefaultConnection();
        [ActionName("PostDiet")]
        [HttpPost]
        public IHttpActionResult PostDiet(DietAndMealCollection dietAndMealCollection)
        {
            var dm = dietAndMealCollection.DietModel;
            var mcm = dietAndMealCollection.MealCollectionModel;
            var user = UserViewModel.GetCurrentUser();
            var dietId = Guid.NewGuid().ToString();
            dm.Id = dietId;
            dm.Author = user.Name;
            dm.User = user.Id;

            _db.Diet.Add(dm);
            foreach (var meal in mcm.Where(x => !string.IsNullOrEmpty(x.Name)))
            {
                    var mealId = Guid.NewGuid().ToString();
                    meal.DietId = dietId;
                    meal.Id = mealId;
                    _db.MealCollection.Add(meal);
            }
            _db.SaveChanges();
            return Ok(dietId);
        }
        [Route("api/DietApi/EditDiet")]
        [HttpGet]
        public IHttpActionResult EditDiet(DietAndMealCollection dietAndMealCollection)
        {
            var dm = dietAndMealCollection.DietModel;
            var mcm = dietAndMealCollection.MealCollectionModel;
            var user = UserViewModel.GetCurrentUser();
            if (dm.User == user.Id)
            {
                var m = new MealCollectionModel();
                foreach (var meal in mcm)
                {
                    m = _db.MealCollection.Find(meal);
                    if (m != null)
                    {
                        m.Amount = meal.Amount;
                        m.Day = meal.Day;
                        m.FoodId = meal.FoodId;
                        m.Edible = meal.Edible;
                        m.Meal = meal.Meal;
                        m.Name = meal.Name;
                        m.WeekNo = meal.WeekNo;
                    }
                    else
                    {
                        _db.MealCollection.Add(meal);
                    }
                }
                _db.Entry(m).State = EntityState.Modified;
                _db.SaveChanges();
                return Ok();
            }
            return BadRequest("Not correct user");
        }
        [Route("api/DietApi/GetDiet")]
        [HttpGet]
        public IHttpActionResult GetDiet(string dietId)
        {
            var mealCollectionModels = _db.MealCollection.Where(x => x.DietId == dietId).ToList();
            var damc = new DietAndMealCollection
            {
                DietModel = _db.Diet.Find(dietId),
                MealCollectionModel = mealCollectionModels
            };
            return Ok(damc);
        }

        //public IEnumerable<DietModel> GetAll()
        //{
        //    return db.Diet.ToList();
        //}
        //public  IHttpActionResult GetDiet(string Id)
        //{
        //    var diet = db.Diet.Find(Id);

        //    if (diet == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(diet);
        //}

        //[HttpPost]
        //public IHttpActionResult PostDiet(JObject jsonData)
        //{
        //    var dm = new DietModel();

        //    dynamic json = jsonData;
        //    var user = UserViewModel.GetCurrentUser();

        //    dm.Id = Guid.NewGuid().ToString();
        //    dm.User = user.Id;
        //    dm.Author = user.Name;
        //    dm.Title = json.title.ToString();
        //    dm.Description = json.description.ToString();
        //    if (json.img != null)
        //    {
        //        dm.Img = json.img.ToString();
        //    }
        //    else
        //    {
        //        dm.Img = "";
        //    }

        //    db.Diet.Add(dm);
        //    db.SaveChanges();
        //    return Ok(dm.Id);
        //}
    }

    public class DietAndMealCollection
    {
        public DietModel DietModel { get; set; }
        public List<MealCollectionModel> MealCollectionModel { get; set; }
    }
}
