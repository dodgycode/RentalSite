namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedBooking : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Admin.Bookings", "InvoiceAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("Admin.Bookings", "InvoiceAmount", c => c.Boolean(nullable: false));
        }
    }
}
