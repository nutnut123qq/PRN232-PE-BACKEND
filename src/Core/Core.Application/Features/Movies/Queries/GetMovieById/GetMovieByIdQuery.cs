using MediatR;
using Core.Application.Common.Interfaces;
using Core.Domain.Entities;

namespace Core.Application.Features.Movies.Queries.GetMovieById;

public class GetMovieByIdQuery : IRequest<Movie?>
{
    public int Id { get; set; }
}

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie?>
{
    private readonly IRepository<Movie> _movieRepository;

    public GetMovieByIdQueryHandler(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Movie?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        return await _movieRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}

