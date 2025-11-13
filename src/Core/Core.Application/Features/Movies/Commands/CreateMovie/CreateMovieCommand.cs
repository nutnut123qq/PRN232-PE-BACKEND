using MediatR;
using Core.Application.Common.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.Movies.Commands.CreateMovie;

public class CreateMovieCommand : IRequest<Movie>
{
    public string Title { get; set; } = string.Empty;
    public string? Genre { get; set; }
    public int? Rating { get; set; }
    public string? PosterUrl { get; set; }
}

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
{
    private readonly IRepository<Movie> _movieRepository;

    public CreateMovieCommandHandler(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Title = request.Title,
            Genre = request.Genre,
            Rating = request.Rating,
            PosterUrl = request.PosterUrl
        };

        return await _movieRepository.AddAsync(movie, cancellationToken);
    }
}

