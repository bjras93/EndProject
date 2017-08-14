using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class moodtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MoodModels", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MoodModels", "Type");
        }
    }
}
