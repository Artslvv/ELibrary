using System;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ELibrary
{
    public class InitializationDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DataContext>>()))
            {
                if (!context.Users.Any())
                {
                    context.Users.Add(new User()
                    {
                        Id = 1,
                        Firstname = "admin",
                        Lastname = "admin",
                        Login = "admin",
                        Password = "admin",
                        Age = 30,
                        Role = 2, //Admin
                    });
                    context.Users.Add(new User()
                    {
                        Id = 2,
                        Firstname = "user",
                        Lastname = "user",
                        Login = "user",
                        Password = "user",
                        Age = 29,
                        Role = 1, //user
                    });
                }

                if (!context.Books.Any())
                {
                    context.Books.Add(new Book()
                    {
                        Price = 200,
                        Purchases = 0,
                        AgeLimit = 18,
                        Name = "Mathematics",
                        Available = true,
                    });
                    context.Books.Add(new Book()
                    {
                        Price = 300,
                        Purchases = 0,
                        AgeLimit = 5,
                        Name = "Physics",
                        Available = false,
                    });
                    context.Books.Add(new Book()
                    {
                        Price = 200,
                        Purchases = 0,
                        AgeLimit = 5,
                        Name = "Red",
                        Available = true,
                    });
                    context.Books.Add(new Book()
                    {
                        Price = 250,
                        Purchases = 0,
                        AgeLimit = 7,
                        Name = "Green",
                        Available = true,
                    });
                }

                context.SaveChanges();
                context.Dispose();
            }
        }
    }
}