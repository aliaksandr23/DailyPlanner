using FluentValidation;
using DailyPlanner.Application.CQRS.Boards.Commands;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Application.Validators.Boards;

internal class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
{
    public UpdateBoardCommandValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
            .WithMessage("Please enter board title")
            .MaximumLength(BoardConstants.MaxTitleLength)
            .WithMessage($"Board title must not exceed {BoardConstants.MaxTitleLength} characters");
        RuleFor(b => b.IsPrivate)
            .NotNull()
            .WithMessage("Field: \"Private\" can't be null");
        RuleFor(b => b.IsFavorite)
            .NotNull()
            .WithMessage("Field: \"Favorite\" can't be null");
    }
}