
namespace Exp.TodoApp.Infrastructure.Persistence;

public class TodoWriteRepository(AppDbContext dbContext) : ITodoWriteRepository
{
    public async Task<int> AddAsync(Todo todo)
    {
        await dbContext.Todos.AddAsync(todo);
        await dbContext.SaveChangesAsync();
        return todo.Id; 
    }

    public async Task<bool> UpdateAsync(Todo todo)
    {
        var exists = await dbContext.Todos.AnyAsync(x => x.Id == todo.Id);
        if (!exists) return false;

        dbContext.Todos.Attach(todo);
        dbContext.Entry(todo).State = EntityState.Modified;

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

