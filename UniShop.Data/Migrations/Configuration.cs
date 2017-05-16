using System;
using System.Collections.Generic;
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

          /*  var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new UniShopDbContext()));

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

            manager.AddToRoles(adminUser.Id, "Admin", "User"); */
            CreateProductCategorySample(context);

        }

        private void CreateProductCategorySample(UniShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true },
                 new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                  new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                   new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }

        }
    }
}