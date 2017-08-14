using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class ArticleUserRequiredFalse : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HealthModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HealthModels", "UserId", c => c.String(nullable: false));
        }
    }
}
