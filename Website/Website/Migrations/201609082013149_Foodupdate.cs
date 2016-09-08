namespace YouGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Foodupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodModels", "Gram", c => c.Int(nullable: false));
            AddColumn("dbo.FoodModels", "Milliliter", c => c.Int(nullable: false));
            AddColumn("dbo.FoodModels", "Pieces", c => c.Int(nullable: false));
            DropColumn("dbo.FoodModels", "Calories");
            DropColumn("dbo.FoodModels", "Measurement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodModels", "Measurement", c => c.String());
            AddColumn("dbo.FoodModels", "Calories", c => c.Int(nullable: false));
            DropColumn("dbo.FoodModels", "Pieces");
            DropColumn("dbo.FoodModels", "Milliliter");
            DropColumn("dbo.FoodModels", "Gram");
        }
    }
}
