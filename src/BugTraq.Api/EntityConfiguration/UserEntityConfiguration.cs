using BugTraq.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTraq.Api.EntityConfiguration 
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.ToTable("Users");
            user.HasKey(u => u.UserId);
            user.HasMany(u => u.Bugs);

            user.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            
            user.Property(u => u.Surname)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}