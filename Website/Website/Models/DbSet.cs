using System.Data.Entity;

namespace YouGo.Models
{
    public class DefaultConnection : DbContext
    {
        public DbSet<DietModel> Diet { get; set; }
        public DbSet<FitnessModel> Fitness { get; set; }
        public DbSet<LikeModel> Heart { get; set; }
        public DbSet<FoodModel> Food { get; set; }
    }
}