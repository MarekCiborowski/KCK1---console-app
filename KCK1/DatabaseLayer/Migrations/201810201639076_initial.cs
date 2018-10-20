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
                        email = c.String(),
                        nickname = c.String(),
                        followers = c.Int(nullable: false),
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
                .ForeignKey("dbo.Account", t => t.accountID, cascadeDelete: true)
                .ForeignKey("dbo.Survey", t => t.surveyID, cascadeDelete: true)
                .Index(t => t.accountID)
                .Index(t => t.surveyID);
            
            CreateTable(
                "dbo.Survey",
                c => new
                    {
                        surveyID = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.surveyID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        questionID = c.Int(nullable: false, identity: true),
                        questionValue = c.String(),
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
                        answerValue = c.String(),
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
                        accountID = c.Int(),
                    })
                .PrimaryKey(t => t.voteID)
                .ForeignKey("dbo.Account", t => t.accountID)
                .ForeignKey("dbo.Answer", t => t.answerID, cascadeDelete: true)
                .Index(t => t.answerID)
                .Index(t => t.accountID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        categoryID = c.Int(nullable: false, identity: true),
                        canAddOwnAnswer = c.Boolean(nullable: false),
                        isSingleChoice = c.Boolean(nullable: false),
                        isAnonymous = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.categoryID);
            
            CreateTable(
                "dbo.FollowedUsers",
                c => new
                    {
                        followedUsersID = c.Int(nullable: false, identity: true),
                        followedUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.followedUsersID)
                .ForeignKey("dbo.Account", t => t.followedUserID, cascadeDelete: true)
                .Index(t => t.followedUserID);
            
            CreateTable(
                "dbo.PersonDatas",
                c => new
                    {
                        personDataID = c.Int(nullable: false),
                        address = c.String(),
                        city = c.String(),
                        zipcode = c.Int(nullable: false),
                        state = c.String(),
                        country = c.String(),
                    })
                .PrimaryKey(t => t.personDataID)
                .ForeignKey("dbo.Account", t => t.personDataID)
                .Index(t => t.personDataID);
            
            CreateTable(
                "dbo.UserSecurities",
                c => new
                    {
                        userSecurityID = c.Int(nullable: false),
                        login = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.userSecurityID)
                .ForeignKey("dbo.Account", t => t.userSecurityID)
                .Index(t => t.userSecurityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSecurities", "userSecurityID", "dbo.Account");
            DropForeignKey("dbo.PersonDatas", "personDataID", "dbo.Account");
            DropForeignKey("dbo.FollowedUsers", "followedUserID", "dbo.Account");
            DropForeignKey("dbo.AccountSurvey", "surveyID", "dbo.Survey");
            DropForeignKey("dbo.Question", "surveyID", "dbo.Survey");
            DropForeignKey("dbo.Question", "categoryID", "dbo.Categories");
            DropForeignKey("dbo.Votes", "answerID", "dbo.Answer");
            DropForeignKey("dbo.Votes", "accountID", "dbo.Account");
            DropForeignKey("dbo.Answer", "questionID", "dbo.Question");
            DropForeignKey("dbo.AccountSurvey", "accountID", "dbo.Account");
            DropIndex("dbo.UserSecurities", new[] { "userSecurityID" });
            DropIndex("dbo.PersonDatas", new[] { "personDataID" });
            DropIndex("dbo.FollowedUsers", new[] { "followedUserID" });
            DropIndex("dbo.Votes", new[] { "accountID" });
            DropIndex("dbo.Votes", new[] { "answerID" });
            DropIndex("dbo.Answer", new[] { "questionID" });
            DropIndex("dbo.Question", new[] { "surveyID" });
            DropIndex("dbo.Question", new[] { "categoryID" });
            DropIndex("dbo.AccountSurvey", new[] { "surveyID" });
            DropIndex("dbo.AccountSurvey", new[] { "accountID" });
            DropTable("dbo.UserSecurities");
            DropTable("dbo.PersonDatas");
            DropTable("dbo.FollowedUsers");
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
