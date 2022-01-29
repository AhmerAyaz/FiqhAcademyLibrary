namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReferenceNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ReferenceNo", c => c.String());
            AddColumn("dbo.Books", "SetNo", c => c.Byte());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "SetNo");
            DropColumn("dbo.Books", "ReferenceNo");
        }
    }
}
