namespace Core.Domain.Entities;

public class Movie : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Genre { get; set; }
    public int? Rating { get; set; }
    public string? PosterUrl { get; set; }
}

