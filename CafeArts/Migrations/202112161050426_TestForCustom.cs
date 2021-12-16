namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestForCustom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customizes", "CreatedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customizes", "CreatedDate");
        }
    }
}
