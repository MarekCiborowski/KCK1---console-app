namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Account", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Account", "nickname", c => c.String(nullable: false));
            AlterColumn("dbo.Survey", "title", c => c.String(nullable: false));
            AlterColumn("dbo.Survey", "description", c => c.String(nullable: false));
            AlterColumn("dbo.Answer", "answerValue", c => c.String(nullable: false));
            AlterColumn("dbo.PersonDatas", "address", c => c.String(nullable: false));
            AlterColumn("dbo.PersonDatas", "city", c => c.String(nullable: false));
            AlterColumn("dbo.PersonDatas", "zipcode", c => c.String(nullable: false));
            AlterColumn("dbo.PersonDatas", "state", c => c.String(nullable: false));
            AlterColumn("dbo.PersonDatas", "country", c => c.String(nullable: false));
            AlterColumn("dbo.UserSecurities", "login", c => c.String(nullable: false));
            AlterColumn("dbo.UserSecurities", "password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserSecurities", "password", c => c.String());
            AlterColumn("dbo.UserSecurities", "login", c => c.String());
            AlterColumn("dbo.PersonDatas", "country", c => c.String());
            AlterColumn("dbo.PersonDatas", "state", c => c.String());
            AlterColumn("dbo.PersonDatas", "zipcode", c => c.String());
            AlterColumn("dbo.PersonDatas", "city", c => c.String());
            AlterColumn("dbo.PersonDatas", "address", c => c.String());
            AlterColumn("dbo.Answer", "answerValue", c => c.String());
            AlterColumn("dbo.Survey", "description", c => c.String());
            AlterColumn("dbo.Survey", "title", c => c.String());
            AlterColumn("dbo.Account", "nickname", c => c.String());
            AlterColumn("dbo.Account", "email", c => c.String());
        }
    }
}
