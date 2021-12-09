namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ReceiptID = c.String(nullable: false, maxLength: 128),
                        CustomerName = c.String(),
                        CustomerEmail = c.String(),
                        CustomerContact = c.String(),
                        CustomerAddress = c.String(),
                        TotalAmount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ReceiptID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
