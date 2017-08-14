using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class ExerciseIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleModels", "ExerciseIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleModels", "ExerciseIndex");
        }
    }
}
