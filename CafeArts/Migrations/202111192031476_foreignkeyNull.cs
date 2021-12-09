namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkeyNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "ProdID", "dbo.ProductModels");
            DropIndex("dbo.Reviews", new[] { "ProdID" });
            AlterColumn("dbo.Reviews", "ProdID", c => c.Int());
            CreateIndex("dbo.Reviews", "ProdID");
            AddForeignKey("dbo.Reviews", "ProdID", "dbo.ProductModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ProdID", "dbo.ProductModels");
            DropIndex("dbo.Reviews", new[] { "ProdID" });
            AlterColumn("dbo.Reviews", "ProdID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "ProdID");
            AddForeignKey("dbo.Reviews", "ProdID", "dbo.ProductModels", "Id", cascadeDelete: true);
        }
    }
}
