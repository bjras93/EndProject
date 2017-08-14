namespace LifeStruct.Models.Fitness
{
    public class ScheduleModel
    {
        public string Id { get; set; }
        public int Day { get; set; }
        public int Week { get; set; }
        public string FitnessId { get; set; }
        public string ExerciseId { get; set; }
        public int ExerciseIndex { get; set; }
        public string Exercise { get; set; }
        public string Calories { get; set; }
        public string Time { get; set; }
    }
}