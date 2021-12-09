namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsValidReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "IsValid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "IsValid");
        }
    }
}
