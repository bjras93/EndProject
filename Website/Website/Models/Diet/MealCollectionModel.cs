namespace YouGo.Models
{
    public class MealCollectionModel
    {
        public string Id { get; set; }
        public int Meal { get; set; }
        public int Day { get; set; }
        public int Edible { get; set; }
        public int WeekNo { get; set; }
        public string FoodId { get; set; }
        public string DietId { get; set; }
        public string Amount { get; set; }

    }
}