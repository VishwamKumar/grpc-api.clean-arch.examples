
namespace Exp.TodoApp.Application.Interfaces.Persistence;

public interface ITodoReadRepository
{
    Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Todo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
