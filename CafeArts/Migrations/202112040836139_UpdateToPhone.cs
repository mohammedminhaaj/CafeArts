namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToPhone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShippingDetails", "PhoneNumber", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShippingDetails", "PhoneNumber", c => c.String(nullable: false));
        }
    }
}
