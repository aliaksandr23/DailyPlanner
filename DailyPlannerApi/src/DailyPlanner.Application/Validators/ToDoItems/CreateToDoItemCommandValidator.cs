using FluentValidation;
using DailyPlanner.Application.CQRS.ToDoItems.Commands;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Application.Validators.ToDoItems;

internal class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
{
    public CreateToDoItemCommandValidator()
    {
        RuleFor(i => i.Title)
            .NotEmpty()
            .WithMessage("Please enter Item title")
            .MaximumLength(ToDoItemConstants.MaxTitleLength)
            .WithMessage($"Item title must not exceed {ToDoItemConstants.MaxTitleLength} characters");
    }
}