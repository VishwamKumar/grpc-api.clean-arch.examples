namespace Exp.TodoApp.Application.Validators;

public class CreateTodoDtoValidator : AbstractValidator<CreateTodoDto>
{
    public CreateTodoDtoValidator()
    {
        RuleFor(x => x.TodoName)
            .NotEmpty().WithMessage("TodoName is required.");
    }
}

