namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LikeDislikeButton : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Rating", c => c.String());
            AlterColumn("dbo.Reviews", "ReviewData", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "ReviewData", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.Reviews", "Rating");
        }
    }
}
