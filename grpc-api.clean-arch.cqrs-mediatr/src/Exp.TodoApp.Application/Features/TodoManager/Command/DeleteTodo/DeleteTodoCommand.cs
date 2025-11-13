
namespace Exp.TodoApp.Application.Features.TodoManager.Command.DeleteTodo;

public record DeleteTodoCommand(int Id) : IRequest<bool>;

