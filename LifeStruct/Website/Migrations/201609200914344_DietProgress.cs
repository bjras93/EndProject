using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class DietProgress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DietProgressModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        DietId = c.String(),
                        CalorieIntake = c.String(),
                        Day = c.String(),
                        FoodId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DietProgressModels");
        }
    }
}
