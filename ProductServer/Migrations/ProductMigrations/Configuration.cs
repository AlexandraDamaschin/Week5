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
    }
}
