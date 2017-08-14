using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class VideoModelType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VideoModels", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VideoModels", "Type");
        }
    }
}
