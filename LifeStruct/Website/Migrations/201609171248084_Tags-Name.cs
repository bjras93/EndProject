namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagsName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DietModels", "Name", c => c.String());
            AddColumn("dbo.DietModels", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DietModels", "Tags");
            DropColumn("dbo.DietModels", "Name");
        }
    }
}
