namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductAndCategoryTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            AddColumn("dbo.ProductModels", "CategoryID", c => c.Int(nullable: false));
            AddColumn("dbo.ProductModels", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductModels", "RibbonStatus", c => c.String());
            AddColumn("dbo.ProductModels", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductModels", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductModels", "ProductDescription", c => c.String());
            AddColumn("dbo.ProductModels", "ProductImage", c => c.Binary());
            AddColumn("dbo.ProductModels", "IsFeatured", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductModels", "ProductQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.ProductModels", "ProductPrice", c => c.Single(nullable: false));
            CreateIndex("dbo.ProductModels", "CategoryID");
            AddForeignKey("dbo.ProductModels", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            DropColumn("dbo.ProductModels", "ProductCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductModels", "ProductCategory", c => c.String());
            DropForeignKey("dbo.ProductModels", "CategoryID", "dbo.Categories");
            DropIndex("dbo.ProductModels", new[] { "CategoryID" });
            DropColumn("dbo.ProductModels", "ProductPrice");
            DropColumn("dbo.ProductModels", "ProductQuantity");
            DropColumn("dbo.ProductModels", "IsFeatured");
            DropColumn("dbo.ProductModels", "ProductImage");
            DropColumn("dbo.ProductModels", "ProductDescription");
            DropColumn("dbo.ProductModels", "ModifiedDate");
            DropColumn("dbo.ProductModels", "CreatedDate");
            DropColumn("dbo.ProductModels", "RibbonStatus");
            DropColumn("dbo.ProductModels", "IsActive");
            DropColumn("dbo.ProductModels", "CategoryID");
            DropTable("dbo.Categories");
        }
    }
}
