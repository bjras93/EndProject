using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class FProgressDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FitnessProgressModels", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FitnessProgressModels", "Date");
        }
    }
}
