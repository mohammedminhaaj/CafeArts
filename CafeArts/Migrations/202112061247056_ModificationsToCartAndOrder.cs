namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificationsToCartAndOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Carts", "OrderID", c => c.Int());
            AddColumn("dbo.Orders", "OrderStatus", c => c.String());
            CreateIndex("dbo.Carts", "OrderID");
            AddForeignKey("dbo.Carts", "OrderID", "dbo.Orders", "OrderID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "OrderID", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "OrderID" });
            DropColumn("dbo.Orders", "OrderStatus");
            DropColumn("dbo.Carts", "OrderID");
            DropColumn("dbo.Carts", "IsActive");
        }
    }
}
