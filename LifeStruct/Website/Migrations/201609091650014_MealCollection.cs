namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MealCollection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MealCollectionModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Meal = c.String(),
                        WeekNo = c.String(),
                        FoodId = c.String(),
                        DietId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MealCollectionModels");
        }
    }
}
