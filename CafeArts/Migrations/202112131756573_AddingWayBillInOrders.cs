namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingWayBillInOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "WayBill", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "WayBill");
        }
    }
}
