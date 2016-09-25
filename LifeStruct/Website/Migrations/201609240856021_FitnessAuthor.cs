namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FitnessAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FitnessModels", "Author", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FitnessModels", "Author");
        }
    }
}
