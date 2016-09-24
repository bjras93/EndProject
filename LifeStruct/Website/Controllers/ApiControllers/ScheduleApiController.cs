namespace LifeStruct.Controllers.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Models;
    public class ScheduleApiController : ApiController
    {
        DefaultConnection db = new DefaultConnection();
        [Route("api/ScheduleApi/FindById")]
        [HttpGet]
        public ScheduleList FindById(string Id)
        {
            ScheduleList sl = new ScheduleList();
            sl.Schedule = db.Schedule.ToList().Where(x => x.FitnessId == Id);
            sl.Exercises = new List<ExerciseModel>();
            foreach(var exercise in sl.Schedule)
            {
                sl.Exercises.Add(db.Exercise.Find(exercise.ExerciseId));
            }

            return sl;
        }
        
    }
    public class ScheduleList
    {
        public List<ExerciseModel> Exercises { get; set; }
        public IEnumerable<ScheduleModel> Schedule { get; set; }
    }
}
