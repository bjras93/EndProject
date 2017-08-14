using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class Author : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietModels", "Author", c => c.String());
            DropColumn("dbo.DietModels", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DietModels", "Name", c => c.String());
            DropColumn("dbo.DietModels", "Author");
        }
    }
}
