using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.EF
{
    class EShopDbContext:DbContext
    {
        public EShopDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Product> Products { get; set; }
        DbSet<AppConfig> AppConfigs { get; set; }
        DbSet<Cart> Carts  { get; set; }
        DbSet<Category> Categorys { get; set; }
        DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Language> Languages  { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<ProductCategory> ProductCategorys { get; set; }
        DbSet<ProductTranslation> ProductTranslations{ get; set; }
        DbSet<Promotion> Promotions { get; set; }
        DbSet<Transaction> Transactions { get; set; }
    }   

}
