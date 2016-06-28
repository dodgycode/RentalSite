namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Admin.Bookings",
                c => new
                    {
                        BookingId = c.Guid(nullable: false),
                        PropertyId = c.Guid(nullable: false),
                        PriceId = c.Guid(nullable: false),
                        bookingSource = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Arrival = c.DateTime(nullable: false),
                        Departure = c.DateTime(nullable: false),
                        EarlyCheckin = c.Boolean(nullable: false),
                        LateCheckout = c.Boolean(nullable: false),
                        Guests = c.Int(nullable: false),
                        DepositAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompleteAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceAmount = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("Admin.Property", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.PropertyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.Bookings", "PropertyId", "Admin.Property");
            DropIndex("Admin.Bookings", new[] { "PropertyId" });
            DropTable("Admin.Bookings");
        }
    }
}
