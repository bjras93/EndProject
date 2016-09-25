namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoodGoal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoalModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MoodModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MoodModels");
            DropTable("dbo.GoalModels");
        }
    }
}
