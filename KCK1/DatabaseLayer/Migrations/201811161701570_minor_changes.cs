namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minor_changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PersonDatas", "isProfilePublic", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Question", "questionValue", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Question", "questionValue", c => c.String());
            DropColumn("dbo.PersonDatas", "isProfilePublic");
        }
    }
}
