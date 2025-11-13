using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Domain.Entities;

namespace Core.Infrastructure.Data.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Users");
        
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasIndex(u => u.Username)
            .IsUnique();
        
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}

