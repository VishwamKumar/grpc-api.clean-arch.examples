using Exp.TodoApp.Application.Features.TodoManager.Command.UpdateTodo;

namespace Exp.TodoApp.Application.Features.TodoManager.Validators;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator(IValidator<UpdateTodoDto> dtoValidator)
    {
        RuleFor(x => x.UpdateDto)
            .NotNull().WithMessage("UpdateTodoDto is required.")
            .SetValidator(dtoValidator);
    }
}