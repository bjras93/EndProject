using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class Fitness : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleModels", "Week", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleModels", "Exercise", c => c.String());
            AlterColumn("dbo.ScheduleModels", "Day", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ScheduleModels", "Day", c => c.String());
            DropColumn("dbo.ScheduleModels", "Exercise");
            DropColumn("dbo.ScheduleModels", "Week");
        }
    }
}
