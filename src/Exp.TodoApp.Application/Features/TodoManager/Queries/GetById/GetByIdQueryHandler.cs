﻿
namespace Exp.TodoApp.Application.Features.TodoManager.Queries.GetById;

public class GetUserByIdQueryHandler(ITodoReadRepository readRepo, IMapper mapper) : IRequestHandler<GetByIdQuery, TodoDto?>
{
    public async Task<TodoDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await readRepo.GetByIdAsync(request.Id);
        return todo is null ? null : mapper.Map<TodoDto>(todo);
    }
}

