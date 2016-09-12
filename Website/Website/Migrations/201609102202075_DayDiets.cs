namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DayDiets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MealCollectionModels", "Day", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MealCollectionModels", "Day");
        }
    }
}
