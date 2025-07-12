
namespace Exp.TodoApp.Application.Features.TodoManager.Queries.GetAll;

public class GetAllQueryHandler(ITodoReadRepository readRepository,
    IMapper mapper) : IRequestHandler<GetAllQuery, List<TodoDto>>
{
    public async Task<List<TodoDto>> Handle(GetAllQuery request,
        CancellationToken cancellationToken)
    {
        var todos = await readRepository.GetAllAsync(); //pass cancellationToken when option is done
        return mapper.Map<List<TodoDto>>(todos);
    }
}
