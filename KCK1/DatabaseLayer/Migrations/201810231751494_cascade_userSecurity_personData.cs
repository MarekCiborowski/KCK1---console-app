namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascade_userSecurity_personData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PersonDatas", "personDataID", "dbo.Account");
            DropForeignKey("dbo.UserSecurities", "userSecurityID", "dbo.Account");
            AddForeignKey("dbo.PersonDatas", "personDataID", "dbo.Account", "accountID", cascadeDelete: true);
            AddForeignKey("dbo.UserSecurities", "userSecurityID", "dbo.Account", "accountID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSecurities", "userSecurityID", "dbo.Account");
            DropForeignKey("dbo.PersonDatas", "personDataID", "dbo.Account");
            AddForeignKey("dbo.UserSecurities", "userSecurityID", "dbo.Account", "accountID");
            AddForeignKey("dbo.PersonDatas", "personDataID", "dbo.Account", "accountID");
        }
    }
}
