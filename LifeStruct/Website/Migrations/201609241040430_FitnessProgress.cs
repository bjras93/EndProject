namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FitnessProgress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FitnessProgressModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FitnessId = c.String(),
                        UserId = c.String(),
                        ExerciseId = c.String(),
                        Loss = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FitnessProgressModels");
        }
    }
}
