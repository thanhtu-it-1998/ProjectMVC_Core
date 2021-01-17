﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig()
                {
                    Key = "HomeTitle",
                    Value = "This is home page of eShopSolution"
                },
                new AppConfig()
                {
                    Key = "HomeKeyword",
                    Value = "This is keyword of eShopSolution"
                },
                new AppConfig()
                {
                    Key = "HomeDescription",
                    Value = "This is description of eShopSolution"
                }
             );

            modelBuilder.Entity<Language>().HasData(
                new Language()
                {
                    Id = "vi-VN",
                    Name = "Tiếng Việt",
                    IsDefault = true
                },
                new Language()
                {
                    Id = "en-US",
                    Name = "English",
                    IsDefault = false
                }
              );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,

                },

                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,

                }
            );
            modelBuilder.Entity<CategoryTranslation>().HasData(
                 new CategoryTranslation()
                 {
                     Id=1,
                     CategoryId = 1,
                     Name = "Áo nam",
                     LanguageId = "vi-VN",
                     SeoAlias = "ao-nam",
                     SeoDescription = "Sản phẩm áo thời trang nam",
                     SeoTitle = "Sản phẩm áo thời trang nam"
                 },
                 new CategoryTranslation()
                 {
                     Id=2,
                     CategoryId = 1,
                     Name = "Men Shirt",
                     LanguageId = "en-US",
                     SeoAlias = "men-shirt",
                     SeoDescription = "The shirt product for men",
                     SeoTitle = "The shirt product for men"
                 },
                 new CategoryTranslation()
                 {
                     Id=3,
                     CategoryId = 2,
                     Name = "Áo nữ",
                     LanguageId = "vi-VN",
                     SeoAlias = "ao-nữ",
                     SeoDescription = "Sản phẩm áo thời trang nữ",
                     SeoTitle = "Sản phẩm áo thời trang nữ"
                 },
                 new CategoryTranslation()
                 {
                     Id=4,
                     CategoryId = 2,
                     Name = "Woman Shirt",
                     LanguageId = "en-US",
                     SeoAlias = "woman-shirt",
                     SeoDescription = "The shirt product for woman",
                     SeoTitle = "The shirt product for woman"
                 }
            );
            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id=1,
                    ProductId=1,
                    Name = "Áo sơ mi nam trắng Việt Tiến",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-so-mi-nam-tran-viet-tien",
                    SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                    SeoTitle = "Áo sơ mi nam trắng Việt Tiến",
                    Description = "Áo sơ mi nam trắng Việt Tiến",
                    Details = "Áo sơ mi nam trắng Việt Tiến"
                },
                new ProductTranslation()
                {
                    Id=2,
                    ProductId=1,
                    Name = "Viet Tien Men T-Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "viet-tien-men-T-Shirt",
                    SeoDescription = "Viet Tien Men T-Shirt",
                    SeoTitle = "Viet Tien Men T-Shirt",
                    Details = "Viet Tien Men T-Shirt",
                    Description = "Viet Tien Men T-Shirt"

                }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 200000,
                    Stock = 0,
                    ViewCount = 0,
                }
            );
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory() { ProductId = 1, CategoryId = 1 });

            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "truongthanhtu.it.1998@hotmail.com",
                NormalizedEmail = "truongthanhtu.it.1998@hotmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Thanh Tu",
                LastName = "Truong",
                Dob = new DateTime(2021,01,17)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }

    }
}
