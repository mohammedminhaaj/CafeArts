namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertingIntoCategory : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CATEGORIES (CATEGORYNAME) VALUES ('Clock')");
            Sql("INSERT INTO CATEGORIES (CATEGORYNAME) VALUES ('Coaster')");
        }
        
        public override void Down()
        {
        }
    }
}
