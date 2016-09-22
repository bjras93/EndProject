namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthModels", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthModels", "Type");
        }
    }
}
