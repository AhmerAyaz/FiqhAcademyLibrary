namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_topic : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Topic");
            DropColumn("dbo.Books", "Column");
            DropColumn("dbo.Books", "Rack");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Rack", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Column", c => c.String(nullable: false));
            AddColumn("dbo.Books", "Topic", c => c.String());
        }
    }
}
