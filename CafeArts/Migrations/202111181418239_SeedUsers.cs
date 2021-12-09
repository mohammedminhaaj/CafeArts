namespace CafeArts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'af42045e-96a1-4205-8864-876c7a469570', N'anonymous@cafearts.com', 0, N'AHLnpII0oSNWjd2thNXwvo3fJ19HwTR2tdleXfaZHLxmabeiZ0uro9phgv4GZSBgDQ==', N'd880b8b4-ed36-4bb1-880c-339c1da4ed28', NULL, 0, 0, NULL, 1, 0, N'anonymous@cafearts.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd7b7222d-a302-4c98-9ae7-f925193b641d', N'mohammedminhaaj97@gmail.com', 0, N'APXBv1AVkDNMEUm6RYPT92ibpVCcZjXi9wO3aRH0hK6WvfmVOR1v6TyudTdk85k1SA==', N'6cb42816-9f31-48eb-8522-9938279a5d98', NULL, 0, 0, NULL, 1, 0, N'mohammedminhaaj97@gmail.com')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9d6d1f57-e63e-4222-bd9a-cf29ef28aaf2', N'Admin')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd7b7222d-a302-4c98-9ae7-f925193b641d', N'9d6d1f57-e63e-4222-bd9a-cf29ef28aaf2')
");
        }
        
        public override void Down()
        {
        }
    }
}
