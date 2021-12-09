namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReviewDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "ReviewCreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "ReviewCreatedDate");
        }
    }
}
