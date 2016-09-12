namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edibleADd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MealCollectionModels", "Edible", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MealCollectionModels", "Edible");
        }
    }
}
