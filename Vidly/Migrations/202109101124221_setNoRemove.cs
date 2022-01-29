namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setNoRemove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "SetNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "SetNo", c => c.Int(nullable: false));
        }
    }
}
