namespace Exp.TodoApp.Application.Interfaces.Persistence;

public interface ITodoRepository
{
    // Read operations
    Task<IEnumerable<Todo>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Todo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    // Write operations
    Task<int> AddAsync(Todo todo, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Todo todo, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

