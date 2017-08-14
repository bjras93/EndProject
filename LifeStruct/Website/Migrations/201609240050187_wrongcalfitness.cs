using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class wrongcalfitness : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleModels", "Calories", c => c.String());
            DropColumn("dbo.FitnessModels", "Calories");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FitnessModels", "Calories", c => c.String());
            DropColumn("dbo.ScheduleModels", "Calories");
        }
    }
}
