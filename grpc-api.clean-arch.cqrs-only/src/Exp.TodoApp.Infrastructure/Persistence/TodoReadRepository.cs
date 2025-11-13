
namespace Exp.TodoApp.Infrastructure.Persistence;

public class TodoReadRepository(AppDbContext dbContext) : ITodoReadRepository
{

    public async Task<IEnumerable<Todo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Todos
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Todo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Todos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }    

}
