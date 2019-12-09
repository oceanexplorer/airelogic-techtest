using System;
using BugTraq.Api.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using BugTraq.Api.Models;

namespace BugTraq.Api.Models
{
    public class BugTraqContext : DbContext
    {
        public BugTraqContext(DbContextOptions<BugTraqContext> options)
            : base(options)
        {            
        }

        public DbSet<Bug> Bugs { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BugEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            
            modelBuilder.Entity<Bug>().HasData(new Bug
            { 
                BugId = 1,
                Title = "Add new company logo to website", 
                Description = "Change the logo on the website", 
                CreatedDate = DateTime.Now,
                Status = "Open",
                UserId = 1
            });

            modelBuilder.Entity<Bug>().HasData(new Bug
            { 
                BugId = 2,
                Title = "Create a backup process", 
                Description = "Create an automated backup process", 
                CreatedDate = DateTime.Now,
                Status = "Open",
                UserId = 1
            });

            modelBuilder.Entity<User>().HasData(new User { UserId = 1, FirstName = "Jeff", Surname = "Simms" });
            modelBuilder.Entity<User>().HasData(new User { UserId = 2, FirstName = "Sally", Surname = "Prescott" });
        }

        public DbSet<BugTraq.Api.Models.User> User { get; set; }
    }
}