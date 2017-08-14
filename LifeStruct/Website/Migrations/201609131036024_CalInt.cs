using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class CalInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExerciseModels", "Calories", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExerciseModels", "Calories", c => c.String());
        }
    }
}
