using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class ImgTagsFitness : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FitnessModels", "Tags", c => c.String());
            AddColumn("dbo.FitnessModels", "Img", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FitnessModels", "Img");
            DropColumn("dbo.FitnessModels", "Tags");
        }
    }
}
