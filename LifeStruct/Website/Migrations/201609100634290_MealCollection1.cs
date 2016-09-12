namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealCollection1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodModels", "Calories", c => c.Int(nullable: false));
            AddColumn("dbo.MealCollectionModels", "Amount", c => c.String());
            DropColumn("dbo.FoodModels", "Gram");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodModels", "Gram", c => c.Int(nullable: false));
            DropColumn("dbo.MealCollectionModels", "Amount");
            DropColumn("dbo.FoodModels", "Calories");
        }
    }
}
