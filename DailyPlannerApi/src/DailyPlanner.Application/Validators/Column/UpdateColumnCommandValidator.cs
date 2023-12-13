using FluentValidation;
using DailyPlanner.Domain.Configuration;
using DailyPlanner.Application.CQRS.Columns.Commands.Update;

namespace DailyPlanner.Application.Validators.Column
{
    public class UpdateColumnCommandValidator : AbstractValidator<UpdateColumnCommand>
    {
        public UpdateColumnCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Please enter column title")
                .MaximumLength(EntitiesConfigurationConstants.MaxColumnTitleLength)
                .WithMessage($"Column title must not exceed {EntitiesConfigurationConstants.MaxCardTitleLength} characters");
        }
    }
}