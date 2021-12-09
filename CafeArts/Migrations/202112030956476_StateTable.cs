namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ShippingDetails", "StateId", c => c.String(nullable: false));
            AddColumn("dbo.ShippingDetails", "states_Id", c => c.Int());
            CreateIndex("dbo.ShippingDetails", "states_Id");
            AddForeignKey("dbo.ShippingDetails", "states_Id", "dbo.States", "Id");
            DropColumn("dbo.ShippingDetails", "State");

            
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShippingDetails", "State", c => c.String(nullable: false, maxLength: 20));
            DropForeignKey("dbo.ShippingDetails", "states_Id", "dbo.States");
            DropIndex("dbo.ShippingDetails", new[] { "states_Id" });
            DropColumn("dbo.ShippingDetails", "states_Id");
            DropColumn("dbo.ShippingDetails", "StateId");
            DropTable("dbo.States");
        }
    }
}
