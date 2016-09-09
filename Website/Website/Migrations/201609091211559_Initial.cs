namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DietModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        User = c.String(),
                        Likes = c.Int(nullable: false),
                        Weeks = c.Int(nullable: false),
                        Img = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FitnessModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Day = c.Int(nullable: false),
                        Meal = c.Int(nullable: false),
                        mealId = c.String(),
                        Calories = c.Int(nullable: false),
                        Edible = c.Int(nullable: false),
                        DietID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DietModels", t => t.DietID)
                .Index(t => t.DietID);
            
            CreateTable(
                "dbo.FoodModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Gram = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LikeModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        ItemId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FitnessModels", "DietID", "dbo.DietModels");
            DropIndex("dbo.FitnessModels", new[] { "DietID" });
            DropTable("dbo.LikeModels");
            DropTable("dbo.FoodModels");
            DropTable("dbo.FitnessModels");
            DropTable("dbo.DietModels");
        }
    }
}
