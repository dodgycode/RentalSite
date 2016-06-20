namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("Admin.PropertyDetails", "summaryText", c => c.String());
            AddColumn("Admin.PropertyDetails", "apartmentText", c => c.String());
            AddColumn("Admin.PropertyDetails", "servicesText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Admin.PropertyDetails", "servicesText");
            DropColumn("Admin.PropertyDetails", "apartmentText");
            DropColumn("Admin.PropertyDetails", "summaryText");
        }
    }
}
