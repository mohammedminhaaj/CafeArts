namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactoring : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShippingDetails", "states_Id", "dbo.States");
            DropIndex("dbo.ShippingDetails", new[] { "states_Id" });
            DropColumn("dbo.ShippingDetails", "StateId");
            DropColumn("dbo.ShippingDetails", "states_Id");
            DropTable("dbo.States");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ShippingDetails", "states_Id", c => c.Int());
            AddColumn("dbo.ShippingDetails", "StateId", c => c.Int(nullable: false));
            CreateIndex("dbo.ShippingDetails", "states_Id");
            AddForeignKey("dbo.ShippingDetails", "states_Id", "dbo.States", "Id");
        }
    }
}
