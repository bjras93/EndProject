namespace LifeStruct.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recipes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipesModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Calories = c.String(),
                        Amount = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecipesModels");
        }
    }
}
