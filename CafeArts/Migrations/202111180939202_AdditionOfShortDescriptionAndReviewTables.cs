namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionOfShortDescriptionAndReviewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        ReviewData = c.String(),
                        ProductID = c.Int(nullable: false),
                        MemberID = c.String(maxLength: 128),
                        products_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberID)
                .ForeignKey("dbo.ProductModels", t => t.products_Id)
                .Index(t => t.MemberID)
                .Index(t => t.products_Id);
            
            AddColumn("dbo.ProductModels", "ProductShortDescription", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "products_Id", "dbo.ProductModels");
            DropForeignKey("dbo.Reviews", "MemberID", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "products_Id" });
            DropIndex("dbo.Reviews", new[] { "MemberID" });
            DropColumn("dbo.ProductModels", "ProductShortDescription");
            DropTable("dbo.Reviews");
        }
    }
}
