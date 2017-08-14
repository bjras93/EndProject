using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class CaloriesFitness : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FitnessModels", "Calories", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FitnessModels", "Calories");
        }
    }
}
