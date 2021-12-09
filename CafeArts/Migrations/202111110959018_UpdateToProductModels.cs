namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToProductModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductModels", "ProductDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.ProductModels", "ProductImage2", c => c.Binary());
            AlterColumn("dbo.ProductModels", "ProductImage3", c => c.Binary());
            AlterColumn("dbo.ProductModels", "ProductImage4", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductModels", "ProductImage4", c => c.Binary(nullable: false));
            AlterColumn("dbo.ProductModels", "ProductImage3", c => c.Binary(nullable: false));
            AlterColumn("dbo.ProductModels", "ProductImage2", c => c.Binary(nullable: false));
            AlterColumn("dbo.ProductModels", "ProductDescription", c => c.String(nullable: false));
        }
    }
}
