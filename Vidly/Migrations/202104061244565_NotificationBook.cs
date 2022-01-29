namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Book", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Book");
        }
    }
}
