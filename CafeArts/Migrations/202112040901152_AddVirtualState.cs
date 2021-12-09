namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVirtualState : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShippingDetails", "StateForShipping_StateKey", "dbo.States");
            DropIndex("dbo.ShippingDetails", new[] { "StateForShipping_StateKey" });
            DropColumn("dbo.ShippingDetails", "StateID");
            RenameColumn(table: "dbo.ShippingDetails", name: "StateForShipping_StateKey", newName: "StateID");
            AlterColumn("dbo.ShippingDetails", "StateID", c => c.Int(nullable: false));
            CreateIndex("dbo.ShippingDetails", "StateID");
            AddForeignKey("dbo.ShippingDetails", "StateID", "dbo.States", "StateKey", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShippingDetails", "StateID", "dbo.States");
            DropIndex("dbo.ShippingDetails", new[] { "StateID" });
            AlterColumn("dbo.ShippingDetails", "StateID", c => c.Int());
            RenameColumn(table: "dbo.ShippingDetails", name: "StateID", newName: "StateForShipping_StateKey");
            AddColumn("dbo.ShippingDetails", "StateID", c => c.Int(nullable: false));
            CreateIndex("dbo.ShippingDetails", "StateForShipping_StateKey");
            AddForeignKey("dbo.ShippingDetails", "StateForShipping_StateKey", "dbo.States", "StateKey");
        }
    }
}
