namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "Request_Id", "dbo.Requests");
            DropIndex("dbo.Notifications", new[] { "Request_Id" });
            DropColumn("dbo.Notifications", "Request_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Request_Id", c => c.Int());
            CreateIndex("dbo.Notifications", "Request_Id");
            AddForeignKey("dbo.Notifications", "Request_Id", "dbo.Requests", "Id");
        }
    }
}
