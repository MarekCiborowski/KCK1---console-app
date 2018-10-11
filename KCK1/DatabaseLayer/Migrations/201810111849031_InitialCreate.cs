namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Nickname = c.String(),
                    })
                .PrimaryKey(t => t.AccountID);
            
            CreateTable(
                "dbo.AccountSurvey",
                c => new
                    {
                        AccountSurveyID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        SurveyID = c.Int(nullable: false),
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
                        Author = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SurveyID);
            
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
                "dbo.Question",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        QuestionValue = c.String(),
                        OptionOfAddingAnswers = c.Boolean(nullable: false),
                        IsSingleChoice = c.Boolean(nullable: false),
                        SurveyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Survey", t => t.SurveyID, cascadeDelete: true)
                .Index(t => t.SurveyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answer", "QuestionID", "dbo.Question");
            DropForeignKey("dbo.Question", "SurveyID", "dbo.Survey");
            DropForeignKey("dbo.AccountSurvey", "SurveyID", "dbo.Survey");
            DropForeignKey("dbo.AccountSurvey", "AccountID", "dbo.Account");
            DropIndex("dbo.Question", new[] { "SurveyID" });
            DropIndex("dbo.Answer", new[] { "QuestionID" });
            DropIndex("dbo.AccountSurvey", new[] { "SurveyID" });
            DropIndex("dbo.AccountSurvey", new[] { "AccountID" });
            DropTable("dbo.Question");
            DropTable("dbo.Answer");
            DropTable("dbo.Survey");
            DropTable("dbo.AccountSurvey");
            DropTable("dbo.Account");
        }
    }
}
