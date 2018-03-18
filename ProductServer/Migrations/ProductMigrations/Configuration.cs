namespace ProductServer.Migrations.ProductMigrations
{
    using ProductServer.Models.ProductModels;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductServer.Models.ProductModels.ProductDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ProductMigrations";
        }

        protected override void Seed(ProductServer.Models.ProductModels.ProductDbContext context)
        {
            SeedSupliers(context);
            SeedProducts(context);
        }

        //seed supliers
        public void SeedSupliers(ProductDbContext context)
        {
            context.Suppliers.AddOrUpdate(s => s.Name,
              new Supplier[] {
                    new Supplier { Name="Tool Man", Address="123 the Beaches"  },
                    new Supplier { Name="Hardware Man", Address="1 the Brook"  },
                    new Supplier { Name="Plumber Man", Address="Unit 12 Ballymun"  }
              });
            context.SaveChanges();
        }

        //seed Products
        public void SeedProducts(ProductDbContext context)
        {
            Random r = new Random();
            context.Products.AddOrUpdate(s => s.Description,
                new Product[] {
                    new Product{  Description="9 inch Nails", SupplierID = GetRandomSupplierID(context), Price = 0.20f, Quantity = r.Next(100,200) },
                    new Product{  Description="Size 9 shoe", SupplierID = GetRandomSupplierID(context), Price = 22.10f, Quantity = r.Next(10,15) },
                    new Product{  Description="Large Overalls", SupplierID = GetRandomSupplierID(context), Price = 42.10f, Quantity = r.Next(10,15) },
                    new Product{  Description="Draining Boards", SupplierID = GetRandomSupplierID(context), Price = 142.10f, Quantity = r.Next(5,7) },
                });
            context.SaveChanges();
        }

        // get a random supplied Id
        private int GetRandomSupplierID(ProductDbContext Context)
        {
            return Context.Suppliers.Select(s => new { s.SupplierID, order = Guid.NewGuid() })
                                    .OrderBy(o => o.order)
                                    .Select(s => s.SupplierID)
                                    .First();
        }
    }
}
