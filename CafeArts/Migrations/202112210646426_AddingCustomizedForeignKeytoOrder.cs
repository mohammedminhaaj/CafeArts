namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCustomizedForeignKeytoOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomizeID", c => c.Int());
            CreateIndex("dbo.Orders", "CustomizeID");
            AddForeignKey("dbo.Orders", "CustomizeID", "dbo.Customizes", "CustomizeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomizeID", "dbo.Customizes");
            DropIndex("dbo.Orders", new[] { "CustomizeID" });
            DropColumn("dbo.Orders", "CustomizeID");
        }
    }
}
