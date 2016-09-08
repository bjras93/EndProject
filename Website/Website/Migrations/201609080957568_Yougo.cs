namespace YouGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Yougo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DietModels", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.DietModels", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DietModels", "Description", c => c.String());
            AlterColumn("dbo.DietModels", "Title", c => c.String());
        }
    }
}
