
namespace Exp.TodoApp.Application.Interfaces.Persistence;

public interface ITodoReadRepository
{
    Task<IEnumerable<Todo>?> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
}
