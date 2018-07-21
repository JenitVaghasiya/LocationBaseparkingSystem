namespace LocationBaseparkingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddParkOnTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.ParkOnVendor",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   UserId = c.String(nullable: false, maxLength: 128),
                   Email = c.String(nullable: false, maxLength: 200),
                   Name = c.String(nullable: false, maxLength: 200),
                   Longitude = c.Decimal(nullable: true, precision: 4, scale: 10),
                   Latitude = c.Decimal(nullable: true, precision: 4, scale: 10),
                   Address = c.String(nullable: true, maxLength: 400),
                   Landmark = c.String(nullable: true, maxLength: 400),
                   NoOfParkingSpace = c.Int(nullable: false),
                   CreatedDate = c.DateTime(),
                   IsActive = c.Boolean(nullable: false, defaultValue: false),
                   HourRate = c.Decimal(nullable: true, precision: 4, scale: 10),
                   Area = c.String(nullable: true, maxLength: 400)
               })
               .PrimaryKey(t => new { t.Id })
               .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
               .Index(t => t.UserId);
        }

        public override void Down()
        {
        }
    }
}
