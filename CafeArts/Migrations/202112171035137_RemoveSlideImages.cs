namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSlideImages : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SlideImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SlideImages",
                c => new
                    {
                        SlideId = c.Int(nullable: false, identity: true),
                        SlideTitle = c.String(),
                        SlidePicture = c.Binary(),
                    })
                .PrimaryKey(t => t.SlideId);
            
        }
    }
}
