using FluentValidation;
using DailyPlanner.Domain.Configuration;
using DailyPlanner.Application.CQRS.Boards.Commands.Update;

namespace DailyPlanner.Application.Validators.Boards
{
    public class UpdateBoardCommandValidator : AbstractValidator<UpdateBoardCommand>
    {
        public UpdateBoardCommandValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().When(b => b.Title is not null)
                .WithMessage("Please enter board title")
                .MaximumLength(EntitiesConfigurationConstants.MaxBoardTitleLength)
                .WithMessage($"Board title must not exceed {EntitiesConfigurationConstants.MaxBoardTitleLength} characters");
        }
    }
}