namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataTypeStateID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShippingDetails", "StateId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShippingDetails", "StateId", c => c.String(nullable: false));
        }
    }
}
