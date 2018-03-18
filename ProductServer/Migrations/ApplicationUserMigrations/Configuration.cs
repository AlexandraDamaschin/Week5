namespace ProductServer.Migrations.ApplicationUserMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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
            //SeedRoles(context);
        }

        //seed users 
        public void SeedUsers(ApplicationDbContext c)
        {
            c.Users.AddOrUpdate(
              p => p.Id,
              new ApplicationUser
              {
                  UserName = "fflyntstone",
                  Email = "flintstone.fred@itsligo.ie",
                  PasswordHash = "Flint$12345",
                  FirstName = "Fred",
                  SecondName = "Flintstone"
              }
            );

            c.SaveChanges();
        }

        //seed roles
        public void SeedRoles(ApplicationDbContext context)
        {
            //create role
            var manager =
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            var roleManager =
               new RoleManager<IdentityRole>(
                   new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole { Name = "PurchasesManager" });

            //assign user to that role
            context.Users.AddOrUpdate(u => u.Email, new ApplicationUser
            {
                UserName = "fflyntstone",
                Email = "flintstone.fred@itsligo.ie",
                PasswordHash = new PasswordHasher().HashPassword("Flint$12345")
            });

            //execption
            ApplicationUser purchasesManager = manager.FindByEmail("flintstone.fred@itsligo.ie");
            if (purchasesManager != null)
            {
                manager.AddToRoles(purchasesManager.Id, new string[] { "PurchasesManager" });
            }
            else
            {
                throw new Exception { Source = "Did not find PurchasesManager" };
            }
            context.SaveChanges();
        }

    }
}
