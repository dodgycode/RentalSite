namespace RentalSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Admin.PropertyImages",
                c => new
                    {
                        ImageId = c.Guid(nullable: false),
                        PropertyId = c.Guid(nullable: false),
                        BlobRef = c.String(nullable: false),
                        Title = c.String(maxLength: 150),
                        Caption = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("Admin.Property", t => t.ImageId)
                .Index(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.PropertyImages", "ImageId", "Admin.Property");
            DropIndex("Admin.PropertyImages", new[] { "ImageId" });
            DropTable("Admin.PropertyImages");
        }
    }
}
