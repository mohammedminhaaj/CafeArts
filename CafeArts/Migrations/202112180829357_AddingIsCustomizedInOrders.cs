namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsCustomizedInOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsCustomized", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsCustomized");
        }
    }
}
