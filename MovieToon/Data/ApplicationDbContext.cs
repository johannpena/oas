using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieToon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieToon.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<MembershipModel> Memberships { get; set; }
        public DbSet<MovieCategoryModel> MovieCategories { get; set; }
        public DbSet<MovieClasificationModel> MovieClasifications { get; set; }
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<RentalModel> Rentals { get; set; }
        public DbSet<RentalMovieModel> RentalMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CustomerModel>().ToTable("Customer");

            builder.Entity<MembershipModel>().ToTable("Membership");
            builder.Entity<MembershipModel>().Property(p => p.Price).HasColumnType("decimal(18,4)");
            builder.Entity<MembershipModel>().Property(p => p.Discount).HasColumnType("decimal(18,4)");

            builder.Entity<MovieCategoryModel>().ToTable("MovieCategory");
            builder.Entity<MovieClasificationModel>().ToTable("MovieClasification");

            builder.Entity<MovieModel>().ToTable("Movie");
            builder.Entity<MovieModel>().Property(p => p.Price).HasColumnType("decimal(18,4)");

            builder.Entity<RentalModel>().ToTable("Rental");

            builder.Entity<RentalMovieModel>().ToTable("RentalMovie");
            builder.Entity<RentalMovieModel>().Property(p => p.Price).HasColumnType("decimal(18,4)");
        }
    }
}
