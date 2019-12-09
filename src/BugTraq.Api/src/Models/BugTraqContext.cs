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
        }

        public DbSet<User> Users { get; set; }
    }
}