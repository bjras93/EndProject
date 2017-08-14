using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class MealDays : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DaysModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MealsModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MealsModels");
            DropTable("dbo.DaysModels");
        }
    }
}
