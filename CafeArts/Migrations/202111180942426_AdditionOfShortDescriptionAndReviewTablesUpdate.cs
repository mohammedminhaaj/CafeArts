namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionOfShortDescriptionAndReviewTablesUpdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reviews", "ProductID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "ProductID", c => c.Int(nullable: false));
        }
    }
}
