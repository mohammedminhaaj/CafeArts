namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCustomize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customizes",
                c => new
                    {
                        CustomizeID = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Color1 = c.String(nullable: false),
                        Color2 = c.String(),
                        Color3 = c.String(),
                        Color4 = c.String(),
                        FullName = c.String(nullable: false),
                        ContactNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        CustomizeDescription = c.String(),
                    })
                .PrimaryKey(t => t.CustomizeID)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customizes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Customizes", new[] { "CategoryId" });
            DropTable("dbo.Customizes");
        }
    }
}
