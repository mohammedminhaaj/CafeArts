namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredReviewData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "ReviewData", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "ReviewData", c => c.String());
        }
    }
}
