namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesToShipping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingDetails", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.ShippingDetails", "OrderID", c => c.Int());
            AlterColumn("dbo.ShippingDetails", "Amount", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShippingDetails", "Amount", c => c.Single(nullable: false));
            AlterColumn("dbo.ShippingDetails", "OrderID", c => c.Int(nullable: false));
            DropColumn("dbo.ShippingDetails", "PhoneNumber");
        }
    }
}
