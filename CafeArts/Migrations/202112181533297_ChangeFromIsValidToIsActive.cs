namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFromIsValidToIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customizes", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customizes", "IsValid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customizes", "IsValid", c => c.Boolean(nullable: false));
            DropColumn("dbo.Customizes", "IsActive");
        }
    }
}
