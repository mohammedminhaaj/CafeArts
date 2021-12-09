namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        MemberID = c.String(maxLength: 128),
                        CartStatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartID)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberID)
                .ForeignKey("dbo.CartStatus", t => t.CartStatusID, cascadeDelete: true)
                .ForeignKey("dbo.ProductModels", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.MemberID)
                .Index(t => t.CartStatusID);
            
            CreateTable(
                "dbo.CartStatus",
                c => new
                    {
                        CartStatusID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.CartStatusID);
            
            CreateTable(
                "dbo.ShippingDetails",
                c => new
                    {
                        ShippingDetailsID = c.Int(nullable: false, identity: true),
                        MemberID = c.String(maxLength: 128),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                        AmountPaid = c.Single(nullable: false),
                        PaymentType = c.String(),
                    })
                .PrimaryKey(t => t.ShippingDetailsID)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberID)
                .Index(t => t.MemberID);
            
            CreateTable(
                "dbo.SlideImages",
                c => new
                    {
                        SlideId = c.Int(nullable: false, identity: true),
                        SlideTitle = c.String(),
                        SlidePicture = c.Binary(),
                    })
                .PrimaryKey(t => t.SlideId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShippingDetails", "MemberID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Carts", "ProductID", "dbo.ProductModels");
            DropForeignKey("dbo.Carts", "CartStatusID", "dbo.CartStatus");
            DropForeignKey("dbo.Carts", "MemberID", "dbo.AspNetUsers");
            DropIndex("dbo.ShippingDetails", new[] { "MemberID" });
            DropIndex("dbo.Carts", new[] { "CartStatusID" });
            DropIndex("dbo.Carts", new[] { "MemberID" });
            DropIndex("dbo.Carts", new[] { "ProductID" });
            DropTable("dbo.SlideImages");
            DropTable("dbo.ShippingDetails");
            DropTable("dbo.CartStatus");
            DropTable("dbo.Carts");
        }
    }
}
