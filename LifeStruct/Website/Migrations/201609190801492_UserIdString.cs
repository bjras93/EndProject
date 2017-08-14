using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class UserIdString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LikeModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LikeModels", "UserId", c => c.Int(nullable: false));
        }
    }
}
