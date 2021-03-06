﻿using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftwareHouse.DataAccess.ContextConfiguration;
using SoftwareHouse.DataAccess.Models;
using SoftwareHouse.DataAccess.Models.LocationInformation;
using SoftwareHouse.DataAccess.Models.UserInformation;

namespace SoftwareHouse.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Experiance> Experiances { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<UserWorkPhoto> UserWorkPhotos { get; set; }

        public DbSet<City> Cities { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
           
            

            builder.ApplyConfiguration(new ExperianceContextConfiguration());
            builder.ApplyConfiguration(new UserContextConfiguration());
            builder.ApplyConfiguration(new UserWorkPhotoContextConfiguration());
            builder.ApplyConfiguration(new UserRatingContextConfiguration());
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
         
            builder.Entity<Project>().HasKey(x => x.Id);

            builder.Entity<Project>().Property(x => x.Name)
                                     .IsRequired();
        }
    }
}