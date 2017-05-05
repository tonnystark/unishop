using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UniShop.Model.Models;

namespace UniShop.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UniShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UniShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new UniShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new UniShopDbContext()));

            var user = new ApplicationUser
            {
                UserName = "uni",
                Email = "uni.international@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Technology Education"
            };

            manager.Create(user, "123654$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole {Name = "Admin"});
                roleManager.Create(new IdentityRole {Name = "User"});
            }

            var adminUser = manager.FindByEmail("uni.international@gmail.com");

            manager.AddToRoles(adminUser.Id, "Admin", "User");
        }
    }
}