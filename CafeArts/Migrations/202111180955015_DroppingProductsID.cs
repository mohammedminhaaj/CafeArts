namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DroppingProductsID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "products_Id", "dbo.ProductModels");
            DropIndex("dbo.Reviews", new[] { "products_Id" });
            RenameColumn(table: "dbo.Reviews", name: "products_Id", newName: "ProdID");
            AlterColumn("dbo.Reviews", "ProdID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "ProdID");
            AddForeignKey("dbo.Reviews", "ProdID", "dbo.ProductModels", "Id", cascadeDelete: true);
            DropColumn("dbo.Reviews", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "ProductID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Reviews", "ProdID", "dbo.ProductModels");
            DropIndex("dbo.Reviews", new[] { "ProdID" });
            AlterColumn("dbo.Reviews", "ProdID", c => c.Int());
            RenameColumn(table: "dbo.Reviews", name: "ProdID", newName: "products_Id");
            CreateIndex("dbo.Reviews", "products_Id");
            AddForeignKey("dbo.Reviews", "products_Id", "dbo.ProductModels", "Id");
        }
    }
}
