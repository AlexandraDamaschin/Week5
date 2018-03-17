namespace ProductServer.Migrations.ProductMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "ReOrderLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "ReOrderLevel", c => c.String());
        }
    }
}
