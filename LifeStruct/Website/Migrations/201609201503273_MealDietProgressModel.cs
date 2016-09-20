namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealDietProgressModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietProgressModels", "Meal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DietProgressModels", "Meal");
        }
    }
}
