using FluentValidation;
using DailyPlanner.Application.CQRS.ToDoItems.Commands;
using static DailyPlanner.Domain.Configuration.EntitiesConfigurationConstants;

namespace DailyPlanner.Application.Validators.ToDoItems;

internal class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator()
    {
        RuleFor(i => i.Title)
            .NotEmpty()
            .WithMessage("Please enter Item title")
            .MaximumLength(ToDoItemConstants.MaxTitleLength)
            .WithMessage($"Item title must not exceed {ToDoItemConstants.MaxTitleLength} characters");
        RuleFor(i => i.IsDone)
            .NotNull()
            .WithMessage("Field: \"IsDone\" can't be null");
    }
}