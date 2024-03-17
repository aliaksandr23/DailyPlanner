using FluentValidation;
using DailyPlanner.Application.CQRS.Columns.Commands;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Application.Validators.Columns;

internal class CreateColumnCommandValidator : AbstractValidator<CreateColumnCommand>
{
    public CreateColumnCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("Please enter column title")
            .MaximumLength(ColumnConstants.MaxTitleLength)
            .WithMessage($"Column title must not exceed {ColumnConstants.MaxTitleLength} characters");
    }
}