using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class VideoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Description = c.String(),
                        Tags = c.String(),
                        YouTubeId = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VideoModels");
        }
    }
}
