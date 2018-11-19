namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        accountID = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false),
                        nickname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.accountID);
            
            CreateTable(
                "dbo.AccountSurvey",
                c => new
                    {
                        accountSurveyID = c.Int(nullable: false, identity: true),
                        accountID = c.Int(nullable: false),
                        surveyID = c.Int(nullable: false),
                        isAuthor = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.accountSurveyID)
                .ForeignKey("dbo.Survey", t => t.surveyID, cascadeDelete: true)
                .ForeignKey("dbo.Account", t => t.accountID, cascadeDelete: true)
                .Index(t => t.accountID)
                .Index(t => t.surveyID);
            
            CreateTable(
                "dbo.Survey",
                c => new
                    {
                        surveyID = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        description = c.String(nullable: false),
                        isAnonymous = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.surveyID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        questionID = c.Int(nullable: false, identity: true),
                        questionValue = c.String(nullable: false),
                        categoryID = c.Int(nullable: false),
                        surveyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.questionID)
                .ForeignKey("dbo.Categories", t => t.categoryID, cascadeDelete: true)
                .ForeignKey("dbo.Survey", t => t.surveyID, cascadeDelete: true)
                .Index(t => t.categoryID)
                .Index(t => t.surveyID);
            
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        answerID = c.Int(nullable: false, identity: true),
                        answerValue = c.String(nullable: false),
                        questionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.answerID)
                .ForeignKey("dbo.Question", t => t.questionID, cascadeDelete: true)
                .Index(t => t.questionID);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        voteID = c.Int(nullable: false, identity: true),
                        answerID = c.Int(nullable: false),
                        accountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.voteID)
                .ForeignKey("dbo.Answer", t => t.answerID, cascadeDelete: true)
                .ForeignKey("dbo.Account", t => t.accountID, cascadeDelete: true)
                .Index(t => t.answerID)
                .Index(t => t.accountID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        categoryID = c.Int(nullable: false, identity: true),
                        canAddOwnAnswer = c.Boolean(nullable: false),
                        isSingleChoice = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.categoryID);
            
            CreateTable(
                "dbo.PersonDatas",
                c => new
                    {
                        personDataID = c.Int(nullable: false),
                        address = c.String(nullable: false),
                        city = c.String(nullable: false),
                        zipcode = c.String(nullable: false),
                        state = c.String(nullable: false),
                        country = c.String(nullable: false),
                        isProfilePublic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.personDataID)
                .ForeignKey("dbo.Account", t => t.personDataID, cascadeDelete: true)
                .Index(t => t.personDataID);
            
            CreateTable(
                "dbo.UserSecurities",
                c => new
                    {
                        userSecurityID = c.Int(nullable: false),
                        login = c.String(nullable: false),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.userSecurityID)
                .ForeignKey("dbo.Account", t => t.userSecurityID, cascadeDelete: true)
                .Index(t => t.userSecurityID);
            
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowerId })
                .ForeignKey("dbo.Account", t => t.UserId)
                .ForeignKey("dbo.Account", t => t.FollowerId)
                .Index(t => t.UserId)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "accountID", "dbo.Account");
            DropForeignKey("dbo.UserSecurities", "userSecurityID", "dbo.Account");
            DropForeignKey("dbo.PersonDatas", "personDataID", "dbo.Account");
            DropForeignKey("dbo.Followers", "FollowerId", "dbo.Account");
            DropForeignKey("dbo.Followers", "UserId", "dbo.Account");
            DropForeignKey("dbo.AccountSurvey", "accountID", "dbo.Account");
            DropForeignKey("dbo.Question", "surveyID", "dbo.Survey");
            DropForeignKey("dbo.Question", "categoryID", "dbo.Categories");
            DropForeignKey("dbo.Votes", "answerID", "dbo.Answer");
            DropForeignKey("dbo.Answer", "questionID", "dbo.Question");
            DropForeignKey("dbo.AccountSurvey", "surveyID", "dbo.Survey");
            DropIndex("dbo.Followers", new[] { "FollowerId" });
            DropIndex("dbo.Followers", new[] { "UserId" });
            DropIndex("dbo.UserSecurities", new[] { "userSecurityID" });
            DropIndex("dbo.PersonDatas", new[] { "personDataID" });
            DropIndex("dbo.Votes", new[] { "accountID" });
            DropIndex("dbo.Votes", new[] { "answerID" });
            DropIndex("dbo.Answer", new[] { "questionID" });
            DropIndex("dbo.Question", new[] { "surveyID" });
            DropIndex("dbo.Question", new[] { "categoryID" });
            DropIndex("dbo.AccountSurvey", new[] { "surveyID" });
            DropIndex("dbo.AccountSurvey", new[] { "accountID" });
            DropTable("dbo.Followers");
            DropTable("dbo.UserSecurities");
            DropTable("dbo.PersonDatas");
            DropTable("dbo.Categories");
            DropTable("dbo.Votes");
            DropTable("dbo.Answer");
            DropTable("dbo.Question");
            DropTable("dbo.Survey");
            DropTable("dbo.AccountSurvey");
            DropTable("dbo.Account");
        }
    }
}
