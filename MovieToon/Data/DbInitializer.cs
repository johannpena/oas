using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieToon.Models;
using Microsoft.AspNetCore.Identity;

namespace MovieToon.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                UserSeed(ref context);
            }
            if (!context.Memberships.Any())
            {
                MembershipSeed(ref context);
            }
            if (!context.MovieCategories.Any())
            {
                MovieCategorySeed(ref context);
            }
            if (!context.MovieClasifications.Any())
            {
                MovieClasificationSeed(ref context);
            }
            context.SaveChanges();
        }

        private static void UserSeed(ref ApplicationDbContext context)
        {
            PasswordHasher<IdentityUser> password = new PasswordHasher<IdentityUser>();

            var adminUser = new IdentityUser { NormalizedUserName = "ADMIN@OAS.ORG", UserName = "admin@oas.org", NormalizedEmail = "ADMIN@OAS.ORG", Email = "admin@oas.org", LockoutEnabled = true };
            adminUser.PasswordHash = password.HashPassword(adminUser, "Admin*123");
            context.Users.Add(adminUser);

            var adminRole = new IdentityRole { Name = "Administrator", NormalizedName = "Administrator" };
            context.Roles.Add(adminRole);

            var adminUserRole = new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = adminRole.Id };
            context.UserRoles.Add(adminUserRole);

            /**********************************************************************************************/

            password = new PasswordHasher<IdentityUser>();
            var emplUser = new IdentityUser { NormalizedUserName = "EMPLOYEE@OAS.ORG", UserName = "employee@oas.org", NormalizedEmail = "EMPLOYEE@OAS.ORG", Email = "employee@oas.org", LockoutEnabled = true };
            emplUser.PasswordHash = password.HashPassword(emplUser, "Employee*123");
            context.Users.Add(emplUser);

            var emplRole = new IdentityRole { Name = "Employee", NormalizedName = "Employee" };
            context.Roles.Add(emplRole);

            var emplUserRole = new IdentityUserRole<string> { UserId = emplUser.Id, RoleId = emplRole.Id };
            context.UserRoles.Add(emplUserRole);
        }

        private static void MembershipSeed(ref ApplicationDbContext context)
        {
            var memberships = new MembershipModel[]
            {
                new MembershipModel{ Name="Pay as you go", Price=0m, Discount=0m },
                new MembershipModel{ Name="Monthly", Price=14.99m, Discount=0.1m },
                new MembershipModel{ Name="Quartely", Price=39.99m, Discount=0.2m },
                new MembershipModel{ Name="Yearly", Price=164.99m, Discount=0.5m },
            };

            foreach (MembershipModel membership in memberships)
            {
                context.Memberships.Add(membership);
            }
        }

        private static void MovieCategorySeed(ref ApplicationDbContext context)
        {
            var movieCategories = new MovieCategoryModel[]
            {
                new MovieCategoryModel{ Name="Action", Description="Action film" },
                new MovieCategoryModel{ Name="Animation", Description="Animation film" },
                new MovieCategoryModel{ Name="Cartoon", Description="Cartoon film" },
                new MovieCategoryModel{ Name="Comedy", Description="Comedy filmm" },
                new MovieCategoryModel{ Name="Horror", Description="Horror film" },
                new MovieCategoryModel{ Name="Thriller", Description="Thriller film" },
                new MovieCategoryModel{ Name="Science fiction", Description="Science fiction film" },
                new MovieCategoryModel{ Name="Romantic", Description="Romantic film" },
            };

            foreach (MovieCategoryModel movieCategory in movieCategories)
            {
                context.MovieCategories.Add(movieCategory);
            }
        }

        private static void MovieClasificationSeed(ref ApplicationDbContext context)
        {
            var movieClasifications = new MovieClasificationModel[]
            {
                new MovieClasificationModel{ Name="G", Description="All public (all ages)" },
                new MovieClasificationModel{ Name="PG", Description="Suggested parental guidance (10 years)" },
                new MovieClasificationModel{ Name="PG-13", Description="Strict parental guidance (13 years)" },
                new MovieClasificationModel{ Name="R", Description="Restricted (17 years)" },
                new MovieClasificationModel{ Name="NC-17", Description="Prohibited for audiences 17 and under (18+ years)" },
                new MovieClasificationModel{ Name="NR", Description="Unclassified" },
            };

            foreach (MovieClasificationModel movieClasification in movieClasifications)
            {
                context.MovieClasifications.Add(movieClasification);
            }
        }
    }
}
