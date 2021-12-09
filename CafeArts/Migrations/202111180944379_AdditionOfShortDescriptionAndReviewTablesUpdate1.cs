namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionOfShortDescriptionAndReviewTablesUpdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "ProductID", c => c.Int(nullable: false));
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "ProductID");
        }
    }
}
