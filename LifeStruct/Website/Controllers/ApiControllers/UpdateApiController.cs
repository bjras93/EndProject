using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using LifeStruct.Models;
using LifeStruct.Models.Account;

namespace LifeStruct.Controllers.ApiControllers
{
    public class UpdateApiController : ApiController
    {
        readonly DefaultConnection _db = new DefaultConnection();

        [Route("api/UpdateApi/SetMood")]
        [HttpGet]
        public IHttpActionResult SetMood(string id)
        {
            var mood = _db.Mood.ToList().Where(x => x.UserId == id.Split('_')[0]);
            MoodModel mm = new MoodModel();
            int type = Convert.ToInt32(id.Split('_')[1]);

            var moodModels = mood as MoodModel[] ?? mood.ToArray();
            if (moodModels.Any())
            {
                if (mm.Date == DateTime.Now.ToString(CultureInfo.InvariantCulture))
                {
                    mm = moodModels.First();
                    mm.Date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    mm.Type = type;
                    _db.Entry(mm).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    mm.Id = Guid.NewGuid().ToString();
                    mm.UserId = id;
                    mm.Date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                    mm.Type = type;
                    _db.Mood.Add(mm);
                    _db.SaveChanges();
                }
            }
            else
            {
                mm.Id = Guid.NewGuid().ToString();
                mm.UserId = id;
                mm.Date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                mm.Type = type;
                _db.Mood.Add(mm);
                _db.SaveChanges();
            }
            return Ok("set");
        }
        [Route("api/UpdateApi/SetGoal")]
        [HttpGet]
        public IHttpActionResult SetGoal(string id)
        {
            var split = id.Split('_');
            var goal = _db.Goal.ToList().Where(x => x.UserId == split[0]);
            GoalModel gm = new GoalModel();
            var Goal = Convert.ToInt32(split[1]);
            var goalModels = goal as GoalModel[] ?? goal.ToArray();
            if (goalModels.Any())
            {
                if (goalModels.First().Date == DateTime.Now.ToString("dd-MM-yyyy"))
                {
                    gm = goalModels.First();
                    gm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                    gm.Goal = Goal;
                    _db.Entry(gm).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    gm.Id = Guid.NewGuid().ToString();
                    gm.UserId = split[0];
                    gm.Goal = Goal;
                    gm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                    _db.Goal.Add(gm);
                    _db.SaveChanges();
                }
            }
            else
            {
                gm.Id = Guid.NewGuid().ToString();
                gm.UserId = split[0];
                gm.Goal = Goal;
                gm.Date = DateTime.Now.ToString("dd-MM-yyyy");
                _db.Goal.Add(gm);
                _db.SaveChanges();
            }
            return Ok();
        }
        [Route("api/UpdateApi/GetMood")]
        [HttpGet]
        public IHttpActionResult GetMood(string uId)
        {
            DateTime dt = DateTime.Now.Date;
            var mood = _db.Mood.ToList().Where(x => x.UserId.Split('_')[0] == uId && Convert.ToDateTime(x.Date).Date.ToString(CultureInfo.InvariantCulture) == dt.ToString(CultureInfo.InvariantCulture) && Convert.ToDateTime(x.Date).Hour > 3);
            if (mood.Any())
            {
                return Ok("set");
            }
            else
            {
                return Ok("notset");
            }

        }
        [Route("api/UpdateApi/GetGoal")]
        [HttpGet]
        public IHttpActionResult GetGoal(string uId)
        {
            var goal = _db.Goal.Where(x => x.UserId == uId);
            var date = new DateTime();
            foreach (var g in goal)
            {
                if (Convert.ToDateTime(g.Date).Date > date)
                {
                    date = Convert.ToDateTime(g.Date);
                }
            }
            var goalByDate = _db.Goal.ToList().Where(x => x.UserId == uId && x.Date == date.ToString("dd-MM-yyyy"));
            var goalModels = goalByDate as GoalModel[] ?? goalByDate.ToArray();
            if (goalModels.Any())
            {
                return Ok(goalModels.First());
            }
            else
            {
                return NotFound();
            }
        }
    }
}
