namespace ProductServer.Migrations.ApplicationUserMigrations
{
    using ProductServer.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductServer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationUserMigrations";
        }

        protected override void Seed(ProductServer.Models.ApplicationDbContext context)
        {
            SeedUsers(context);
        }

        //seed users 
        public void SeedUsers(ApplicationDbContext c)
        {
            c.Users.AddOrUpdate(
              p => p.Id,
              new ApplicationUser { UserName = "fflyntstone", Email = "flintstone.fred@itsligo.ie", PasswordHash = "Flint$12345" }
            );
        }
    }
}
