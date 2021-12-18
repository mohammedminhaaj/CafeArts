namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesToCustomizeProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customizes", "FullName", c => c.String());
            AlterColumn("dbo.Customizes", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customizes", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Customizes", "FullName", c => c.String(nullable: false));
        }
    }
}
