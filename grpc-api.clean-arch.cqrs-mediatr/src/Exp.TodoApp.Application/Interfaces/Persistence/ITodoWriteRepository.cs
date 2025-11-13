
namespace Exp.TodoApp.Application.Interfaces.Persistence;

public interface ITodoWriteRepository
{
    Task<int> AddAsync(Todo todo, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Todo todo, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
