namespace Exp.TodoApp.Application.Features.TodoManager.Queries.GetById;

public record GetByIdQuery(int Id) : IRequest<TodoDto>;
