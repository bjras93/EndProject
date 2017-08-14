namespace LifeStruct.Models.Diet
{
    public class DietProgressModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string DietId { get; set; }
        public string CalorieIntake { get; set; }
        public string Day { get; set; }
        public string FoodId { get; set; }
        public int Meal { get; set; }
    }
}