namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDeliveryDateToOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeliveryDate");
        }
    }
}
