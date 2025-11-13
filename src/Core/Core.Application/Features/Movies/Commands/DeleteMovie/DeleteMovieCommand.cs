using MediatR;
using Core.Application.Common.Interfaces;

namespace Core.Application.Features.Movies.Commands.DeleteMovie;

public class DeleteMovieCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Unit>
{
    private readonly IRepository<Core.Domain.Entities.Movie> _movieRepository;

    public DeleteMovieCommandHandler(IRepository<Core.Domain.Entities.Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (movie == null)
        {
            throw new KeyNotFoundException($"Movie with ID {request.Id} not found.");
        }

        await _movieRepository.DeleteAsync(movie, cancellationToken);
        return Unit.Value;
    }
}

