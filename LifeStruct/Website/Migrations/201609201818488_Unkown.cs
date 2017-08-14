using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class Unkown : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VideoModels", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.VideoModels", "YouTubeId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VideoModels", "YouTubeId", c => c.String());
            AlterColumn("dbo.VideoModels", "Title", c => c.String());
        }
    }
}
