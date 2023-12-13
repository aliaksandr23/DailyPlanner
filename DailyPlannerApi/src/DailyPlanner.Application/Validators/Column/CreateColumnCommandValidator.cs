using FluentValidation;
using DailyPlanner.Domain.Configuration;
using DailyPlanner.Application.CQRS.Columns.Commands.Create;

namespace DailyPlanner.Application.Validators.Column
{
    public class CreateColumnCommandValidator : AbstractValidator<CreateColumnCommand>
    {
        public CreateColumnCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Please enter column title")
                .MaximumLength(EntitiesConfigurationConstants.MaxColumnTitleLength)
                .WithMessage($"Column title must not exceed {EntitiesConfigurationConstants.MaxCardTitleLength} characters");
        }
    }
}