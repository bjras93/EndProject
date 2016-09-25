namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalModels", "Goal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalModels", "Goal");
        }
    }
}
