namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cdg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Admin.PropertyImages", "ImageId", "Admin.Property");
            DropColumn("Admin.PropertyImages", "PropertyId");
            RenameColumn(table: "Admin.PropertyImages", name: "ImageId", newName: "PropertyId");
            RenameIndex(table: "Admin.PropertyImages", name: "IX_ImageId", newName: "IX_PropertyId");
            AddColumn("Admin.Property", "Active", c => c.Boolean(nullable: false));
            AddColumn("Admin.PropertyImages", "ImageURL", c => c.String(nullable: false));
            AddColumn("Admin.PropertyImages", "Active", c => c.Boolean(nullable: false));
            AddForeignKey("Admin.PropertyImages", "PropertyId", "Admin.Property", "PropertyId", cascadeDelete: true);
            DropColumn("Admin.PropertyImages", "BlobRef");
        }
        
        public override void Down()
        {
            AddColumn("Admin.PropertyImages", "BlobRef", c => c.String(nullable: false));
            DropForeignKey("Admin.PropertyImages", "PropertyId", "Admin.Property");
            DropColumn("Admin.PropertyImages", "Active");
            DropColumn("Admin.PropertyImages", "ImageURL");
            DropColumn("Admin.Property", "Active");
            RenameIndex(table: "Admin.PropertyImages", name: "IX_PropertyId", newName: "IX_ImageId");
            RenameColumn(table: "Admin.PropertyImages", name: "PropertyId", newName: "ImageId");
            AddColumn("Admin.PropertyImages", "PropertyId", c => c.Guid(nullable: false));
            AddForeignKey("Admin.PropertyImages", "ImageId", "Admin.Property", "PropertyId");
        }
    }
}
