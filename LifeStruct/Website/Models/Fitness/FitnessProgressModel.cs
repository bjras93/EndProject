namespace LifeStruct
{
    public class FitnessProgressModel
    {
        public string Id { get; set; }
        public string FitnessId { get; set; }
        public string UserId { get; set; }
        public string ExerciseId { get; set; }
        public decimal Loss { get; set; }
        public string Date { get; set; }
    }
}