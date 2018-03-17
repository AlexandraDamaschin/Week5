namespace ProductServer.Migrations.ProductMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialProductMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Quantity = c.Int(nullable: false),
                        ReOrderLevel = c.String(),
                        Price = c.Single(nullable: false),
                        SupplierID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Supplier", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.SupplierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "SupplierID", "dbo.Supplier");
            DropIndex("dbo.Product", new[] { "SupplierID" });
            DropTable("dbo.Supplier");
            DropTable("dbo.Product");
        }
    }
}
