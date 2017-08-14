using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class Upd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MealCollectionModels", "Meal", c => c.Int(nullable: false));
            AlterColumn("dbo.MealCollectionModels", "WeekNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MealCollectionModels", "WeekNo", c => c.String());
            AlterColumn("dbo.MealCollectionModels", "Meal", c => c.String());
        }
    }
}
