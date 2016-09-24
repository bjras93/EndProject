namespace Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BmiClass : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BmiClassModels");
        }
    }
}
