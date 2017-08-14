using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class Like : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LikeModels", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.LikeModels", "TypeId", c => c.String());
            AlterColumn("dbo.LikeModels", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.LikeModels", "ItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LikeModels", "ItemId", c => c.String());
            AlterColumn("dbo.LikeModels", "UserId", c => c.String());
            DropColumn("dbo.LikeModels", "TypeId");
            DropColumn("dbo.LikeModels", "Type");
        }
    }
}
