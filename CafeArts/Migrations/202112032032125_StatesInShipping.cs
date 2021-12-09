namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatesInShipping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingDetails", "StID", c => c.Int(nullable: false));
            AddColumn("dbo.ShippingDetails", "StateForShipping_StateKey", c => c.Int());
            CreateIndex("dbo.ShippingDetails", "StateForShipping_StateKey");
            AddForeignKey("dbo.ShippingDetails", "StateForShipping_StateKey", "dbo.States", "StateKey");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShippingDetails", "StateForShipping_StateKey", "dbo.States");
            DropIndex("dbo.ShippingDetails", new[] { "StateForShipping_StateKey" });
            DropColumn("dbo.ShippingDetails", "StateForShipping_StateKey");
            DropColumn("dbo.ShippingDetails", "StID");
        }
    }
}
