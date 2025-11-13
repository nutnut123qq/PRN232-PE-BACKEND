using MediatR;
using Core.Application.Common.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.Movies.Commands.UpdateMovie;

public class UpdateMovieCommand : IRequest<Movie>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Genre { get; set; }
    public int? Rating { get; set; }
    public string? PosterUrl { get; set; }
}

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Movie>
{
    private readonly IRepository<Movie> _movieRepository;

    public UpdateMovieCommandHandler(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Movie> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (movie == null)
        {
            throw new KeyNotFoundException($"Movie with ID {request.Id} not found.");
        }

        movie.Title = request.Title;
        movie.Genre = request.Genre;
        movie.Rating = request.Rating;
        movie.PosterUrl = request.PosterUrl;

        await _movieRepository.UpdateAsync(movie, cancellationToken);
        return movie;
    }
}

