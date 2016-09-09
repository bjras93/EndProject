namespace YouGo.Models
{
    public class FitnessModel
    {
        public string Id { get; set; }
        public int Day { get; set; }
        public int Meal { get; set; }
        public string mealId { get; set; }
        public int Calories { get; set; }
        public int Edible { get; set; }
        public string DietID { get; set; }
        public virtual DietModel Diet { get; set; }
    }
}