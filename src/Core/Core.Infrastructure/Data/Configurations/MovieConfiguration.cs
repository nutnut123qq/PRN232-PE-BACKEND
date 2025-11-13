using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Domain.Entities;

namespace Core.Infrastructure.Data.Configurations;

public class MovieConfiguration : BaseEntityConfiguration<Movie>
{
    public override void Configure(EntityTypeBuilder<Movie> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Movies");
        
        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(m => m.Genre)
            .HasMaxLength(50);
        
        builder.Property(m => m.Rating)
            .IsRequired(false);
        
        builder.Property(m => m.PosterUrl)
            .HasMaxLength(500);
        
        // Add check constraint for rating range (1-5)
        builder.ToTable(t => t.HasCheckConstraint("CK_Movies_Rating", "Rating IS NULL OR (Rating >= 1 AND Rating <= 5)"));
    }
}
