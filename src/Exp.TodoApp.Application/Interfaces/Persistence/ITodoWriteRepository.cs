
namespace Exp.TodoApp.Application.Interfaces.Persistence;

public interface ITodoWriteRepository
{
    Task<int> AddAsync(Todo todo);
    Task<bool> UpdateAsync(Todo todo);
    Task<bool> DeleteAsync(int id);
}
