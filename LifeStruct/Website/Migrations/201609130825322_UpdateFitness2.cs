namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFitness2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExerciseModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Calories = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScheduleModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Day = c.String(),
                        FitnessId = c.String(),
                        ExerciseId = c.String(),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.FitnessModels", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.FitnessModels", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FitnessModels", "Description", c => c.String());
            AlterColumn("dbo.FitnessModels", "Title", c => c.String());
            DropTable("dbo.ScheduleModels");
            DropTable("dbo.ExerciseModels");
        }
    }
}
