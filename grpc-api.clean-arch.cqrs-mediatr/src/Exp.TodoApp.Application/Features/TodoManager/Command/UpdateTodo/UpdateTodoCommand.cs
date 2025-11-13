
namespace Exp.TodoApp.Application.Features.TodoManager.Command.UpdateTodo;

public record UpdateTodoCommand(UpdateTodoDto UpdateDto) : IRequest<bool>;

