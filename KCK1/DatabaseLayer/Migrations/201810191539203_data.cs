namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class data : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Account", "Followers", c => c.Int(nullable: false));
            AddColumn("dbo.AccountSurvey", "IsAuthor", c => c.Boolean(nullable: false));
            AddColumn("dbo.Question", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Question", "CategoryID");
            AddForeignKey("dbo.Question", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            DropColumn("dbo.Account", "Login");
            DropColumn("dbo.Account", "Password");
            DropColumn("dbo.Survey", "Author");
            DropColumn("dbo.Question", "OptionOfAddingAnswers");
            DropColumn("dbo.Question", "IsSingleChoice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "IsSingleChoice", c => c.Boolean(nullable: false));
            AddColumn("dbo.Question", "OptionOfAddingAnswers", c => c.Boolean(nullable: false));
            AddColumn("dbo.Survey", "Author", c => c.String());
            AddColumn("dbo.Account", "Password", c => c.String());
            AddColumn("dbo.Account", "Login", c => c.String());
            DropForeignKey("dbo.UserSecurities", "UserSecurityID", "dbo.Account");
            DropForeignKey("dbo.PersonDatas", "PersonDataID", "dbo.Account");
            DropForeignKey("dbo.FollowedUsers", "FollowedUserID", "dbo.Account");
            DropForeignKey("dbo.Question", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Votes", "AnswerID", "dbo.Answer");
            DropForeignKey("dbo.Votes", "AccountID", "dbo.Account");
            DropIndex("dbo.UserSecurities", new[] { "UserSecurityID" });
            DropIndex("dbo.PersonDatas", new[] { "PersonDataID" });
            DropIndex("dbo.FollowedUsers", new[] { "FollowedUserID" });
            DropIndex("dbo.Votes", new[] { "AccountID" });
            DropIndex("dbo.Votes", new[] { "AnswerID" });
            DropIndex("dbo.Question", new[] { "CategoryID" });
            DropColumn("dbo.Question", "CategoryID");
            DropColumn("dbo.AccountSurvey", "IsAuthor");
            DropColumn("dbo.Account", "Followers");
            DropTable("dbo.UserSecurities");
            DropTable("dbo.PersonDatas");
            DropTable("dbo.FollowedUsers");
            DropTable("dbo.Categories");
            DropTable("dbo.Votes");
        }
    }
}
