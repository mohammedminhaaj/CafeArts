namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToProductModels1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductModels", "ProductImage1", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductModels", "ProductImage1", c => c.Binary(nullable: false));
        }
    }
}
