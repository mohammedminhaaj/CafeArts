namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProductModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductModels", "ProductImage1", c => c.Binary(nullable: false));
            AddColumn("dbo.ProductModels", "ProductImage2", c => c.Binary(nullable: false));
            AddColumn("dbo.ProductModels", "ProductImage3", c => c.Binary(nullable: false));
            AddColumn("dbo.ProductModels", "ProductImage4", c => c.Binary(nullable: false));
            AlterColumn("dbo.ProductModels", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.ProductModels", "ModifiedDate", c => c.DateTime());
            DropColumn("dbo.ProductModels", "ProductCost");
            DropColumn("dbo.ProductModels", "ProductImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductModels", "ProductImage", c => c.Binary(nullable: false));
            AddColumn("dbo.ProductModels", "ProductCost", c => c.Double(nullable: false));
            AlterColumn("dbo.ProductModels", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductModels", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ProductModels", "ProductImage4");
            DropColumn("dbo.ProductModels", "ProductImage3");
            DropColumn("dbo.ProductModels", "ProductImage2");
            DropColumn("dbo.ProductModels", "ProductImage1");
        }
    }
}
