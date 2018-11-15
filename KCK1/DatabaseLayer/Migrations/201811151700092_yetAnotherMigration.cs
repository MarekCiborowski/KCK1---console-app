namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yetAnotherMigration : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AccountSurvey");
            AlterColumn("dbo.AccountSurvey", "accountSurveyID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.AccountSurvey", "accountSurveyID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AccountSurvey");
            AlterColumn("dbo.AccountSurvey", "accountSurveyID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.AccountSurvey", new[] { "accountID", "surveyID" });
        }
    }
}
