namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Admin.Address",
                c => new
                    {
                        AddressId = c.Guid(nullable: false),
                        PropertyId = c.Guid(nullable: false),
                        AddressLine1 = c.String(maxLength: 80),
                        AddressLine2 = c.String(maxLength: 80),
                        AddressLine3 = c.String(maxLength: 80),
                        AddressLine4 = c.String(maxLength: 80),
                        AddressLine5 = c.String(maxLength: 80),
                        Postcode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("Admin.Property", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "Admin.Property",
                c => new
                    {
                        PropertyId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 80),
                    })
                .PrimaryKey(t => t.PropertyId);
            
            CreateTable(
                "Admin.PropertyDetails",
                c => new
                    {
                        PropertyDetailsId = c.Guid(nullable: false),
                        PropertyId = c.Guid(nullable: false),
                        NumSleeps = c.Int(nullable: false),
                        NumBedrooms = c.Int(nullable: false),
                        NumBathrooms = c.Int(nullable: false),
                        Parking = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyDetailsId)
                .ForeignKey("Admin.Property", t => t.PropertyDetailsId)
                .Index(t => t.PropertyDetailsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.PropertyDetails", "PropertyDetailsId", "Admin.Property");
            DropForeignKey("Admin.Address", "AddressId", "Admin.Property");
            DropIndex("Admin.PropertyDetails", new[] { "PropertyDetailsId" });
            DropIndex("Admin.Address", new[] { "AddressId" });
            DropTable("Admin.PropertyDetails");
            DropTable("Admin.Property");
            DropTable("Admin.Address");
        }
    }
}
