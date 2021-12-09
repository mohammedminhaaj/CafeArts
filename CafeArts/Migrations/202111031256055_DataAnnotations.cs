namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductModels", "ProductName", c => c.String(nullable: false));
            AlterColumn("dbo.ProductModels", "ProductDescription", c => c.String(nullable: false));
            AlterColumn("dbo.ProductModels", "ProductImage", c => c.Binary(nullable: false));
            AlterColumn("dbo.ShippingDetails", "Address", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.ShippingDetails", "City", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ShippingDetails", "State", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ShippingDetails", "Country", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ShippingDetails", "ZipCode", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.ShippingDetails", "PaymentType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShippingDetails", "PaymentType", c => c.String());
            AlterColumn("dbo.ShippingDetails", "ZipCode", c => c.Int(nullable: false));
            AlterColumn("dbo.ShippingDetails", "Country", c => c.String());
            AlterColumn("dbo.ShippingDetails", "State", c => c.String());
            AlterColumn("dbo.ShippingDetails", "City", c => c.String());
            AlterColumn("dbo.ShippingDetails", "Address", c => c.String());
            AlterColumn("dbo.ProductModels", "ProductImage", c => c.Binary());
            AlterColumn("dbo.ProductModels", "ProductDescription", c => c.String());
            AlterColumn("dbo.ProductModels", "ProductName", c => c.String());
        }
    }
}
