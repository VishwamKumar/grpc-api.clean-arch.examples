
namespace Exp.TodoApp.Infrastructure.Persistence;

public class TodoReadRepository(AppDbContext dbContext) : ITodoReadRepository
{

    public async Task<IEnumerable<Todo>?> GetAllAsync()
    {
        return await dbContext.Todos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Todo?> GetByIdAsync(int id)
    {
        return await dbContext.Todos
        .AsNoTracking()
        .FirstOrDefaultAsync(t => t.Id == id);
    }    

}
