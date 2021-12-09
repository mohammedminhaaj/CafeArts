namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStates : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO STATES (STATENAME) VALUES ('ANDAMAN AND NICOBAR ISLANDS'),
('ANDHRA PRADESH'),
('ARUNACHAL PRADESH'),
('ASSAM'),
('BIHAR'),
('CHATTISGARH'),
('CHANDIGARH'),
('DAMAN AND DIU'),
('DELHI'),
('DADRA AND NAGAR HAVELI'),
('GOA'),
('GUJARAT'),
('HIMACHAL PRADESH'),
('HARYANA'),
('JAMMU AND KASHMIR'),
('JHARKHAND'),
('KERALA'),
('KARNATAKA'),
('LAKSHADWEEP'),
('MEGHALAYA'),
('MAHARASHTRA'),
('MANIPUR'),
('MADHYA PRADESH'),
('MIZORAM'),
('NAGALAND'),
('ORISSA'),
('PUNJAB'),
('PONDICHERRY'),
('RAJASTHAN'),
('SIKKIM'),
('TAMIL NADU'),
('TRIPURA'),
('UTTARAKHAND'),
('UTTAR PRADESH'),
('WEST BENGAL'),
('TELANGANA'); ");
        }
        
        public override void Down()
        {
        }
    }
}
