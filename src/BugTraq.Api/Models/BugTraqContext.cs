using System;
using System.Collections.Generic;
using BugTraq.Api.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

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
    }

    public class Bug
    {
        public int BugId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } 

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class User
    {
        public int UserId { get; set;}
        public string FirstName { get; set; }
        public string Surname { get; set; }
        
        public virtual List<Bug> Bugs { get; set; }
    }
}