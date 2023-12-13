using FluentValidation;
using DailyPlanner.Domain.Configuration;
using DailyPlanner.Application.CQRS.Boards.Commands.Create;

namespace DailyPlanner.Application.Validators.Boards
{
    public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
    {
        public CreateBoardCommandValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Please enter board title")
                .MaximumLength(EntitiesConfigurationConstants.MaxBoardTitleLength)
                .WithMessage($"Board title must not exceed {EntitiesConfigurationConstants.MaxBoardTitleLength} characters");
        }
    }
}