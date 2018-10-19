namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Nickname = c.String(),
                        Followers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID);
            
            CreateTable(
                "dbo.AccountSurvey",
                c => new
                    {
                        AccountSurveyID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        SurveyID = c.Int(nullable: false),
                        IsAuthor = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountSurveyID)
                .ForeignKey("dbo.Account", t => t.AccountID, cascadeDelete: true)
                .ForeignKey("dbo.Survey", t => t.SurveyID, cascadeDelete: true)
                .Index(t => t.AccountID)
                .Index(t => t.SurveyID);
            
            CreateTable(
                "dbo.Survey",
                c => new
                    {
                        SurveyID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SurveyID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        QuestionValue = c.String(),
                        CategoryID = c.Int(nullable: false),
                        SurveyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Survey", t => t.SurveyID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.SurveyID);
            
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        AnswerValue = c.String(),
                        QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Question", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VotesID = c.Int(nullable: false, identity: true),
                        AnswerID = c.Int(nullable: false),
                        AccountID = c.Int(),
                    })
                .PrimaryKey(t => t.VotesID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .ForeignKey("dbo.Answer", t => t.AnswerID, cascadeDelete: true)
                .Index(t => t.AnswerID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CanAddOwnAnswer = c.Boolean(nullable: false),
                        IsSingleChoice = c.Boolean(nullable: false),
                        IsAnonymous = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.FollowedUsers",
                c => new
                    {
                        FollowedUsersID = c.Int(nullable: false, identity: true),
                        FollowedUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FollowedUsersID)
                .ForeignKey("dbo.Account", t => t.FollowedUserID, cascadeDelete: true)
                .Index(t => t.FollowedUserID);
            
            CreateTable(
                "dbo.PersonDatas",
                c => new
                    {
                        PersonDataID = c.Int(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        Zipcode = c.Int(nullable: false),
                        State = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.PersonDataID)
                .ForeignKey("dbo.Account", t => t.PersonDataID)
                .Index(t => t.PersonDataID);
            
            CreateTable(
                "dbo.UserSecurities",
                c => new
                    {
                        UserSecurityID = c.Int(nullable: false),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserSecurityID)
                .ForeignKey("dbo.Account", t => t.UserSecurityID)
                .Index(t => t.UserSecurityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSecurities", "UserSecurityID", "dbo.Account");
            DropForeignKey("dbo.PersonDatas", "PersonDataID", "dbo.Account");
            DropForeignKey("dbo.FollowedUsers", "FollowedUserID", "dbo.Account");
            DropForeignKey("dbo.AccountSurvey", "SurveyID", "dbo.Survey");
            DropForeignKey("dbo.Question", "SurveyID", "dbo.Survey");
            DropForeignKey("dbo.Question", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Votes", "AnswerID", "dbo.Answer");
            DropForeignKey("dbo.Votes", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Answer", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.AccountSurvey", "AccountID", "dbo.Account");
            DropIndex("dbo.UserSecurities", new[] { "UserSecurityID" });
            DropIndex("dbo.PersonDatas", new[] { "PersonDataID" });
            DropIndex("dbo.FollowedUsers", new[] { "FollowedUserID" });
            DropIndex("dbo.Votes", new[] { "AccountID" });
            DropIndex("dbo.Votes", new[] { "AnswerID" });
            DropIndex("dbo.Answer", new[] { "QuestionID" });
            DropIndex("dbo.Question", new[] { "SurveyID" });
            DropIndex("dbo.Question", new[] { "CategoryID" });
            DropIndex("dbo.AccountSurvey", new[] { "SurveyID" });
            DropIndex("dbo.AccountSurvey", new[] { "AccountID" });
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
