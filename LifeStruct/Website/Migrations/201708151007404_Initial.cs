namespace LifeStruct.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                    Multiplier = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.BmiClassModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                    Start = c.Decimal(nullable: false, precision: 18, scale: 2),
                    End = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DaysModels",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DietModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Title = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    User = c.String(),
                    Author = c.String(),
                    Likes = c.Int(nullable: false),
                    Weeks = c.Int(nullable: false),
                    Tags = c.String(),
                    Img = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.DietProgressModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(),
                    DietId = c.String(),
                    CalorieIntake = c.String(),
                    Day = c.String(),
                    FoodId = c.String(),
                    Meal = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ExerciseModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                    Calories = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.FitnessModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Title = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    UserId = c.String(),
                    Tags = c.String(),
                    Img = c.String(),
                    Author = c.String(),
                    Likes = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.FitnessProgressModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FitnessId = c.String(),
                    UserId = c.String(),
                    ExerciseId = c.String(),
                    Loss = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Date = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.FoodModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                    Calories = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.GoalModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(),
                    Goal = c.Int(nullable: false),
                    Date = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.HealthModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Title = c.String(nullable: false),
                    Content = c.String(nullable: false),
                    UserId = c.String(),
                    Tags = c.String(),
                    Likes = c.Int(nullable: false),
                    Type = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.LikeModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(),
                    Type = c.Int(nullable: false),
                    TypeId = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.MealCollectionModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Meal = c.Int(nullable: false),
                    Day = c.Int(nullable: false),
                    Edible = c.Int(nullable: false),
                    WeekNo = c.Int(nullable: false),
                    FoodId = c.String(),
                    DietId = c.String(),
                    Amount = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.MealsModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.MoodModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(),
                    Date = c.String(),
                    Type = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ScheduleModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Day = c.Int(nullable: false),
                    Week = c.Int(nullable: false),
                    FitnessId = c.String(),
                    ExerciseId = c.String(),
                    ExerciseIndex = c.Int(nullable: false),
                    Exercise = c.String(),
                    Calories = c.String(),
                    Time = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.VideoModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Title = c.String(nullable: false),
                    Description = c.String(),
                    Tags = c.String(),
                    YouTubeId = c.String(nullable: false),
                    UserId = c.String(),
                    Type = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.WeightModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    UserId = c.String(),
                    Date = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.WeightModels");
            DropTable("dbo.VideoModels");
            DropTable("dbo.ScheduleModels");
            DropTable("dbo.MoodModels");
            DropTable("dbo.MealsModels");
            DropTable("dbo.MealCollectionModels");
            DropTable("dbo.LikeModels");
            DropTable("dbo.HealthModels");
            DropTable("dbo.GoalModels");
            DropTable("dbo.FoodModels");
            DropTable("dbo.FitnessProgressModels");
            DropTable("dbo.FitnessModels");
            DropTable("dbo.ExerciseModels");
            DropTable("dbo.DietProgressModels");
            DropTable("dbo.DietModels");
            DropTable("dbo.DaysModels");
            DropTable("dbo.BmiClassModels");
            DropTable("dbo.ActivityModels");
        }
    }
}
