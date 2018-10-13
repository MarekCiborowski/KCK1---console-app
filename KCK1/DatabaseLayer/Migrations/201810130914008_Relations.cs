namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountSurvey", "isAuthor", c => c.Boolean(nullable: false));
            AddColumn("dbo.Answer", "numberOfVotes", c => c.Int(nullable: false));
            DropColumn("dbo.Survey", "Author");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Survey", "Author", c => c.String());
            DropColumn("dbo.Answer", "numberOfVotes");
            DropColumn("dbo.AccountSurvey", "isAuthor");
        }
    }
}
