using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class edibleADd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MealCollectionModels", "Edible", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MealCollectionModels", "Edible");
        }
    }
}
