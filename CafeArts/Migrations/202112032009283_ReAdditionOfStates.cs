namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReAdditionOfStates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateKey = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateKey);

            Sql(@"INSERT INTO STATES (STATENAME) VALUES ('Andaman and Nicobar Islands'),
('Andhra Pradesh'),
('Arunachal Pradesh'),
('Assam'),
('Bihar'),
('Chattisgarh'),
('Chandigarh'),
('Daman and Diu'),
('Delhi'),
('Dadra and Nagar Haveli'),
('Goa'),
('Gujarat'),
('Himachal Pradesh'),
('Haryana'),
('Jammu and Kashmir'),
('Jharkhand'),
('Kerala'),
('Karnataka'),
('Lakshadweep'),
('Meghalaya'),
('Maharashtra'),
('Manipur'),
('Madhya Pradesh'),
('Mizoram'),
('Nagaland'),
('Orissa'),
('Punjab'),
('Pondicherry'),
('Rajasthan'),
('Sikkim'),
('Tamil Nadu'),
('Tripura'),
('Uttarakhand'),
('Uttar Pradesh'),
('West Bengal'),
('Telangana'); ");

        }
        
        public override void Down()
        {
            DropTable("dbo.States");
        }
    }
}
