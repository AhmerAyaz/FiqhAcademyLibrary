namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "SetNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "SetNo");
        }
    }
}
