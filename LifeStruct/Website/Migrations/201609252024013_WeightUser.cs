using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class WeightUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeightModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WeightModels");
        }
    }
}
