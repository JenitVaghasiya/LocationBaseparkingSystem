namespace LocationBaseparkingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddParkOnTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.ParkOnVendors",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   UserId = c.String(nullable: false, maxLength: 128),
                   Email = c.String(nullable: false, maxLength: 200),
                   Name = c.String(nullable: false, maxLength: 200),
                   Longitude = c.Decimal(nullable: true, precision: 18, scale: 2),
                   Latitude = c.Decimal(nullable: true, precision: 18, scale: 2),
                   Address = c.String(nullable: true, maxLength: 400),
                   Landmark = c.String(nullable: true, maxLength: 400),
                   NoOfParkingSpace = c.Int(nullable: false),
                   CreatedDate = c.DateTime(),
                   IsActive = c.Boolean(nullable: false, defaultValue: false),
                   HourRate = c.Decimal(nullable: true, precision: 18, scale: 2),
                   Area = c.String(nullable: true, maxLength: 400)
               })
               .PrimaryKey(t => new { t.Id })
               .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true);

            CreateTable(
               "dbo.ParkOnVendorTrans",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   VenderID = c.Int(nullable: false),
                   CustomerName = c.String(nullable: false, maxLength: 200),
                   PhoneNo = c.String(nullable: false, maxLength: 200),
                   CarNo = c.String(nullable: false, maxLength: 200),
                   EntryTime = c.DateTime(defaultValue: DateTime.Now),
                   ExitTime = c.DateTime(nullable: true),
                   IsOut = c.Boolean(nullable: false, defaultValue: false),
                   TotalHours = c.Decimal(nullable: true),
                   TotalCost = c.Decimal(nullable: true)
               })
               .PrimaryKey(t => new { t.Id });

            AddForeignKey("dbo.ParkOnVendorTrans", "VenderID", "dbo.ParkOnVendors", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.ParkOnVendor", "UserId", "dbo.AspNetUsers");
            DropTable("dbo.ParkOnVendor");

            DropForeignKey("dbo.ParkOnVendorTrans", "VenderID", "dbo.ParkOnVendor");
            DropTable("dbo.ParkOnVendorTrans");
        }
    }
}
