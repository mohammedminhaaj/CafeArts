namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyingShippingDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingDetails", "Amount", c => c.Single(nullable: false));
            AddColumn("dbo.ShippingDetails", "ShippingType", c => c.String(nullable: false));
            DropColumn("dbo.ShippingDetails", "AmountPaid");
            DropColumn("dbo.ShippingDetails", "PaymentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShippingDetails", "PaymentType", c => c.String(nullable: false));
            AddColumn("dbo.ShippingDetails", "AmountPaid", c => c.Single(nullable: false));
            DropColumn("dbo.ShippingDetails", "ShippingType");
            DropColumn("dbo.ShippingDetails", "Amount");
        }
    }
}
