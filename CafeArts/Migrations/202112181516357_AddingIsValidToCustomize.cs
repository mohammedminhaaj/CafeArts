namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIsValidToCustomize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customizes", "IsValid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customizes", "IsValid");
        }
    }
}
