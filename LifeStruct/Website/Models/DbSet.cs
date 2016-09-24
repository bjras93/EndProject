using System.Data.Entity;

namespace LifeStruct.Models
{
    public class DefaultConnection : DbContext
    {
        public DbSet<DietModel> Diet { get; set; }
        public DbSet<FitnessModel> Fitness { get; set; }
        public DbSet<BmiClassModel> BmiClass { get; set; }
        public DbSet<FoodModel> Food { get; set; }
        public DbSet<MealsModel> Meals { get; set; }
        public DbSet<DaysModel> Days { get; set; }
        public DbSet<ExerciseModel> Exercise { get; set; }
        public DbSet<ScheduleModel> Schedule { get; set; }
        public DbSet<MealCollectionModel> MealCollection { get; set; }
        public DbSet<LikeModel> Like { get; set; }
        public DbSet<DietProgressModel> DietProgress { get; set; }
        public DbSet<VideoModel> Video { get; set; }
        public DbSet<HealthModel> Health { get; set; }
    }
}