namespace ProductServer.Migrations.ApplicationUserMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraFieldsNeededForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecondName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SecondName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
