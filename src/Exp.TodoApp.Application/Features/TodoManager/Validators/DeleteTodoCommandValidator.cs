using Exp.TodoApp.Application.Features.TodoManager.Command.DeleteTodo;

namespace Exp.TodoApp.Application.Features.TodoManager.Validators;

public class DeleteTodoCommandValidator : AbstractValidator<DeleteTodoCommand>
{
    public DeleteTodoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid Todo Id.");
    }
}