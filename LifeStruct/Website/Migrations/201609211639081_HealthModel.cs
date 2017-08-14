using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class HealthModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HealthModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        Tags = c.String(),
                        Likes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HealthModels");
        }
    }
}
