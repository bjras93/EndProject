using System.Data.Entity.Migrations;

namespace LifeStruct.Migrations
{
    public partial class FitnessAdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FitnessModels", "DietID", "dbo.DietModels");
            DropIndex("dbo.FitnessModels", new[] { "DietID" });
            AddColumn("dbo.FitnessModels", "Title", c => c.String());
            AddColumn("dbo.FitnessModels", "Description", c => c.String());
            AddColumn("dbo.FitnessModels", "ScheduleId", c => c.String());
            DropColumn("dbo.FitnessModels", "Day");
            DropColumn("dbo.FitnessModels", "Meal");
            DropColumn("dbo.FitnessModels", "mealId");
            DropColumn("dbo.FitnessModels", "Calories");
            DropColumn("dbo.FitnessModels", "Edible");
            DropColumn("dbo.FitnessModels", "DietID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FitnessModels", "DietID", c => c.String(maxLength: 128));
            AddColumn("dbo.FitnessModels", "Edible", c => c.Int(nullable: false));
            AddColumn("dbo.FitnessModels", "Calories", c => c.Int(nullable: false));
            AddColumn("dbo.FitnessModels", "mealId", c => c.String());
            AddColumn("dbo.FitnessModels", "Meal", c => c.Int(nullable: false));
            AddColumn("dbo.FitnessModels", "Day", c => c.Int(nullable: false));
            DropColumn("dbo.FitnessModels", "ScheduleId");
            DropColumn("dbo.FitnessModels", "Description");
            DropColumn("dbo.FitnessModels", "Title");
            CreateIndex("dbo.FitnessModels", "DietID");
            AddForeignKey("dbo.FitnessModels", "DietID", "dbo.DietModels", "Id");
        }
    }
}
