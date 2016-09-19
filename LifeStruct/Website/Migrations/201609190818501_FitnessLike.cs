namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FitnessLike : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FitnessModels", "Likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FitnessModels", "Likes");
        }
    }
}
