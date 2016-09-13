namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFitness1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FitnessModels", "ScheduleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FitnessModels", "ScheduleId", c => c.String());
        }
    }
}
