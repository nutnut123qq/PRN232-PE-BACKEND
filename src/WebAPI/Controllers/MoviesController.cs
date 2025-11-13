using Microsoft.AspNetCore.Mvc;
using MediatR;
using Core.Application.Features.Movies.Queries.GetAllMovies;
using Core.Application.Features.Movies.Queries.GetMovieById;
using Core.Application.Features.Movies.Commands.CreateMovie;
using Core.Application.Features.Movies.Commands.UpdateMovie;
using Core.Application.Features.Movies.Commands.DeleteMovie;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Core.Domain.Entities.Movie>>> GetAllMovies(
        [FromQuery] string? search,
        [FromQuery] string? genre,
        [FromQuery] string? sortBy,
        [FromQuery] string? sortOrder)
    {
        var query = new GetAllMoviesQuery
        {
            Search = search,
            Genre = genre,
            SortBy = sortBy,
            SortOrder = sortOrder
        };
        
        var movies = await _mediator.Send(query);
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Core.Domain.Entities.Movie>> GetMovieById(int id)
    {
        var query = new GetMovieByIdQuery { Id = id };
        var movie = await _mediator.Send(query);
        
        if (movie == null)
        {
            return NotFound();
        }
        
        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<Core.Domain.Entities.Movie>> CreateMovie([FromBody] CreateMovieCommand command)
    {
        var movie = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Core.Domain.Entities.Movie>> UpdateMovie(int id, [FromBody] UpdateMovieCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Id mismatch");
        }

        try
        {
            var movie = await _mediator.Send(command);
            return Ok(movie);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var command = new DeleteMovieCommand { Id = id };
        
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}

