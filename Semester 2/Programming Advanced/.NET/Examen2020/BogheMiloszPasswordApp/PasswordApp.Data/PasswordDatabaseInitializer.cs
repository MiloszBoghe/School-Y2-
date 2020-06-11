using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace PasswordApp.Data
{
    public static class PasswordDatabaseInitializer
    {
        public static void SeedData(UserManager<User> userManager)
        {
            var jeff = new User
            {
                UserName = "jeff@localhost",
                Email = "jeff@localhost",
                Entries = new List<Entry>
                {
                    new Entry
                    {
                        Id = Guid.Parse("e294be14-ea6e-4941-adec-a1467b481a25"),
                        Title = "Outlook account",
                        UserName = "jeff@outlook.com",
                        Password = "4HdYlFO14BzTP3WhLA2DNg==", //SomePassword
                        Url = "https://www.outlook.com"
                    },
                    new Entry
                    {
                        Id = Guid.Parse("72a8ebd8-1ac0-41ad-a5ee-2984cb3c32d7"),
                        Title = "Google account",
                        UserName = "jeff@gmail.com",
                        Password = "htzUOSi+m70O8BqkaizZqg==", //SomePassword
                        Url = "https://www.google.be"
                    }
                }
            };
            AddUserIfNotExists(userManager, jeff, "Exam123!");

            var marie = new User
            {
                UserName = "marie@localhost",
                Email = "marie@localhost",
                Entries = new List<Entry>
                {
                    new Entry
                    {
                        Id = Guid.Parse("3c48f88d-0c85-4bda-841b-ec9ca913fbbc"),
                        Title = "Outlook account",
                        UserName = "marie@outlook.com",
                        Password = "JIZwLcF4bLw8JZkW6Nq6JA==", //SecretPassword
                        Url = "https://www.outlook.com"
                    },
                    new Entry
                    {
                        Id = Guid.Parse("473c70d9-90b1-4343-a727-a5afecdd1cd5"),
                        Title = "Facebook account",
                        UserName = "marie@outlook.com",
                        Password = "F+4TnGQw5SStWrvktzMmvg==", //SecretPassword
                        Url = "https://www.facebook.be"
                    }
                }
            };
            AddUserIfNotExists(userManager, marie, "Exam123!");
        }

        private static void AddUserIfNotExists(UserManager<User> userManager, User user, string password)
        {
            var result = userManager.CreateAsync(user, password).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Debug.WriteLine(error.Description);
                }
            }
        }
    }
}