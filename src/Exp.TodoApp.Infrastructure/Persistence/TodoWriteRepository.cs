
namespace Exp.TodoApp.Infrastructure.Persistence;

public class TodoWriteRepository(AppDbContext dbContext) : ITodoWriteRepository
{
    public async Task<int> AddAsync(Todo todo)
    {
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync();
        return todo.Id; // assuming Id is auto-generated
    }

    public async Task<bool> UpdateAsync(Todo todo)
    {
        dbContext.Todos.Update(todo);
        var result = await dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todo = await dbContext.Todos.FindAsync(id);
        if (todo == null)
            return false;

        dbContext.Todos.Remove(todo);
        var result = await dbContext.SaveChangesAsync();
        return result > 0;
    }
}

