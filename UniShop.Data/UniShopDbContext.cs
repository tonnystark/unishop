﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using UniShop.Model.Models;

namespace UniShop.Data
{
    public class UniShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public UniShopDbContext() : base("DBConnect")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Footer> Footers { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Product> Products { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }

        public DbSet<Tag> Tags { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }

        public static UniShopDbContext Create()
        {
            return new UniShopDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>().HasKey(u => new {u.UserId, u.RoleId});
            modelBuilder.Entity<IdentityUserLogin>().HasKey(u => u.UserId);
        }
    }
}