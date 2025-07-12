namespace Exp.TodoApp.Application.Features.TodoManager.Command.CreateTodo;

public class UpdateTodoCommandHandler(
    ITodoWriteRepository writeRepo   
) : IRequestHandler<CreateTodoCommand, int>
{
    public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateDto;
        Todo todo = new();

        if (!string.IsNullOrEmpty(dto.TodoName))
        {
            todo.ToDoName = dto.TodoName;
            await writeRepo.AddAsync(todo);
            return todo.Id;
        }
        return 0;
    }
}