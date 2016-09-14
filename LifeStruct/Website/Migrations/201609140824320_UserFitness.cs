namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFitness : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FitnessModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FitnessModels", "UserId");
        }
    }
}
