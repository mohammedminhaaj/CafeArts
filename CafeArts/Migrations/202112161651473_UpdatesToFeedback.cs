namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesToFeedback : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customizes", "ContactNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Feedbacks", "Contact", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "Contact", c => c.String(nullable: false));
            AlterColumn("dbo.Customizes", "ContactNumber", c => c.String(nullable: false));
        }
    }
}
