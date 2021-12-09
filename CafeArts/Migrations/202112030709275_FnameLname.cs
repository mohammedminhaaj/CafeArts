namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FnameLname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShippingDetails", "FName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.ShippingDetails", "LName", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShippingDetails", "LName");
            DropColumn("dbo.ShippingDetails", "FName");
        }
    }
}
