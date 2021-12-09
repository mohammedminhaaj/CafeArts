namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "CartStatusID", "dbo.CartStatus");
            DropForeignKey("dbo.Carts", "ProductID", "dbo.ProductModels");
            DropIndex("dbo.Carts", new[] { "ProductID" });
            DropIndex("dbo.Carts", new[] { "CartStatusID" });
            AlterColumn("dbo.Carts", "ProductID", c => c.Int());
            CreateIndex("dbo.Carts", "ProductID");
            AddForeignKey("dbo.Carts", "ProductID", "dbo.ProductModels", "Id");
            DropColumn("dbo.Carts", "CartStatusID");
            DropTable("dbo.CartStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartStatus",
                c => new
                    {
                        CartStatusID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.CartStatusID);
            
            AddColumn("dbo.Carts", "CartStatusID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Carts", "ProductID", "dbo.ProductModels");
            DropIndex("dbo.Carts", new[] { "ProductID" });
            AlterColumn("dbo.Carts", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "CartStatusID");
            CreateIndex("dbo.Carts", "ProductID");
            AddForeignKey("dbo.Carts", "ProductID", "dbo.ProductModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "CartStatusID", "dbo.CartStatus", "CartStatusID", cascadeDelete: true);
        }
    }
}
