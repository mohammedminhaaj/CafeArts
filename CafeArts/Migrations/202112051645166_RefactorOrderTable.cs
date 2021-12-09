namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorOrderTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        RazorPayKey = c.String(),
                        MemberID = c.String(maxLength: 128),
                        TransactionID = c.String(),
                        RPUniquePaymentID = c.String(),
                        CustomerName = c.String(),
                        CustomerEmail = c.String(),
                        CustomerContact = c.String(),
                        CustomerAddress = c.String(),
                        TotalAmount = c.Single(nullable: false),
                        CreatedDate = c.DateTime(),
                        OrderType = c.String(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberID)
                .Index(t => t.MemberID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "MemberID", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "MemberID" });
            DropTable("dbo.Orders");
        }
    }
}
