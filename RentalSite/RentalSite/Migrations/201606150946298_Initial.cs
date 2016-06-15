namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId);
            
            CreateTable(
                "Admin.PropertyDetails",
                c => new
                    {
                        DetailsId = c.Guid(nullable: false),
                        PropertyId = c.Guid(nullable: false),
                        NumSleeps = c.Int(nullable: false),
                        NumBedrooms = c.Int(nullable: false),
                        NumBathrooms = c.Int(nullable: false),
                        Parking = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DetailsId)
                .ForeignKey("Admin.Property", t => t.DetailsId)
                .Index(t => t.DetailsId);
            
            CreateTable(
                "Admin.PropertyImages",
                c => new
                    {
                        PropertyImageId = c.Guid(nullable: false),
                        PropertyId = c.Guid(nullable: false),
                        ImageURL = c.String(nullable: false),
                        Title = c.String(maxLength: 150),
                        Caption = c.String(maxLength: 250),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyImageId)
                .ForeignKey("Admin.Property", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.PropertyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.PropertyImages", "PropertyId", "Admin.Property");
            DropForeignKey("Admin.PropertyDetails", "DetailsId", "Admin.Property");
            DropForeignKey("Admin.Address", "AddressId", "Admin.Property");
            DropIndex("Admin.PropertyImages", new[] { "PropertyId" });
            DropIndex("Admin.PropertyDetails", new[] { "DetailsId" });
            DropIndex("Admin.Address", new[] { "AddressId" });
            DropTable("Admin.PropertyImages");
            DropTable("Admin.PropertyDetails");
            DropTable("Admin.Property");
            DropTable("Admin.Address");
        }
    }
}
