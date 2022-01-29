namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Character : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Column", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Column");
        }
    }
}
