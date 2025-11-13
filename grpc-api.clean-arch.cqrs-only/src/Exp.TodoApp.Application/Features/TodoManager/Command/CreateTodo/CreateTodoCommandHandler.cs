namespace Exp.TodoApp.Application.Features.TodoManager.Command.CreateTodo;

public class CreateTodoCommandHandler(
    ITodoWriteRepository writeRepo   
) : ICommandHandler<CreateTodoCommand, int>
{
    public async Task<int> Handle(CreateTodoCommand command, CancellationToken cancellationToken = default)
    {
        var dto = command.CreateDto;
        Todo todo = Todo.Create(dto.TodoName);
        var id = await writeRepo.AddAsync(todo, cancellationToken);
        return id;        
    }
}