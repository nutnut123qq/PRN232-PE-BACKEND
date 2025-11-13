using FluentValidation;

namespace Core.Application.Features.Movies.Commands.UpdateMovie;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.Genre)
            .MaximumLength(50).WithMessage("Genre must not exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Genre));

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.")
            .When(x => x.Rating.HasValue);

        RuleFor(x => x.PosterUrl)
            .Must(BeValidUrl).WithMessage("PosterUrl must be a valid URL.")
            .MaximumLength(500).WithMessage("PosterUrl must not exceed 500 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.PosterUrl));
    }

    private bool BeValidUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return true;

        return Uri.TryCreate(url, UriKind.Absolute, out var result) &&
               (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}

