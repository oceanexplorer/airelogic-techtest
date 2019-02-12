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
                Title = "First Bug", 
                Description = "This is our first bug!", 
                CreatedDate = DateTime.Now,
                Status = "Open",
                UserId = 1
            });

            modelBuilder.Entity<Bug>().HasData(new Bug
            { 
                BugId = 2,
                Title = "Second Bud", 
                Description = "This is our second bug!", 
                CreatedDate = DateTime.Now,
                Status = "Open",
                UserId = 1
            });

            modelBuilder.Entity<User>().HasData(new User { UserId = 1, FirstName = "Jeff", Surname = "Simms" });
        }

        public DbSet<BugTraq.Api.Models.User> User { get; set; }
    }
}