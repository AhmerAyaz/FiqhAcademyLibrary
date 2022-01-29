namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnAlphabet : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "Column");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Column", c => c.Int(nullable: false));
        }
    }
}
