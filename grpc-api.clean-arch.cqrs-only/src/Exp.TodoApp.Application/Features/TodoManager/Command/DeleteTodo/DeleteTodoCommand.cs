namespace Exp.TodoApp.Application.Features.TodoManager.Command.DeleteTodo;

public record DeleteTodoCommand(int Id) : ICommand<bool>;

