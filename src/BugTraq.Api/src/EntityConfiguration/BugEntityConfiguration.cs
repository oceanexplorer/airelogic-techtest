using BugTraq.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTraq.Api.EntityConfiguration 
{
    public class BugEntityConfiguration : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> bug)
        {
            bug.HasKey(b => b.BugId);
            bug.HasOne(b => b.User)
                .WithMany(u => u.Bugs)
                .HasForeignKey(e => e.UserId);

            bug.Property(b => b.Title)
                .HasMaxLength(75)
                .IsRequired();
            
            bug.Property(b => b.Description)
                .HasMaxLength(300)
                .IsRequired();

            bug.Property(b => b.Status)
                .HasMaxLength(20)
                .IsRequired();

            bug.Property(b => b.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("getutcdate()");
        }
    }
}