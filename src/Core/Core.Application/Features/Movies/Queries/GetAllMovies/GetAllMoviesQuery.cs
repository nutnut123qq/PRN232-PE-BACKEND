using System.Linq;
using MediatR;
using Core.Application.Common.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.Movies.Queries.GetAllMovies;

public class GetAllMoviesQuery : IRequest<List<Movie>>
{
    public string? Search { get; set; }
    public string? Genre { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
}

public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, List<Movie>>
{
    private readonly IRepository<Movie> _movieRepository;

    public GetAllMoviesQueryHandler(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<List<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetAllAsync(cancellationToken);
        IEnumerable<Movie> query = movies;

        // Search by title (case-insensitive)
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(m => m.Title.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
        }

        // Filter by genre
        if (!string.IsNullOrWhiteSpace(request.Genre))
        {
            query = query.Where(m => m.Genre != null && m.Genre.Equals(request.Genre, StringComparison.OrdinalIgnoreCase));
        }

        // Sort
        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            var isAscending = request.SortOrder?.ToLower() != "desc";
            
            query = request.SortBy.ToLower() switch
            {
                "rating" => isAscending 
                    ? query.OrderBy(m => m.Rating ?? 0)
                    : query.OrderByDescending(m => m.Rating ?? 0),
                "title" => isAscending
                    ? query.OrderBy(m => m.Title)
                    : query.OrderByDescending(m => m.Title),
                _ => query.OrderBy(m => m.Id)
            };
        }
        else
        {
            query = query.OrderBy(m => m.Id);
        }

        return query.ToList();
    }
}

