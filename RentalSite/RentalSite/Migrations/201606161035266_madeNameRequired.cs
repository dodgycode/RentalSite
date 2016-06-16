namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Admin.Property", "Name", c => c.String(nullable: false, maxLength: 80));
        }
        
        public override void Down()
        {
            AlterColumn("Admin.Property", "Name", c => c.String(maxLength: 80));
        }
    }
}
