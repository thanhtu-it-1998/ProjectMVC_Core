using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    Id = 1,
                    Name = "The Perks of Being a Wallflower",
                    Author = "YA edition",
                    ISBN="1313464131",
                }, new Book()
                {
                    Id = 2,
                    Name = " Harry Potter",
                    Author = " Paperback Boxed Set Books ",
                    ISBN="6479446515",
                }, new Book()
                {
                    Id = 3,
                    Name = "Collins Easy Learning English Conversation",
                    Author = "National Geographic",
                    ISBN="7996524635",
                });
        }
    }
}
