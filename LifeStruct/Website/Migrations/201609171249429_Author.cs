namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
