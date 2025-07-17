using Exp.TodoApp.Application.Features.TodoManager.Command.CreateTodo;
using Exp.TodoApp.Application.Features.TodoManager.Dtos;
using Exp.TodoApp.Application.Interfaces.Persistence;
using Exp.TodoApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Exp.TodoApp.Tests.IntegrationTests.Persistence;

public class CreateTodoCommandHandlerTests
{
    private readonly AppDbContext _context;
    private readonly ITodoWriteRepository _writeRepo;
    private readonly CreateTodoCommandHandler _handler;

    public CreateTodoCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: $"TodoDb_{Guid.NewGuid()}")
            .Options;

        _context = new AppDbContext(options);
        _writeRepo = new TodoWriteRepository(_context);
        _handler = new CreateTodoCommandHandler(_writeRepo);
    }

    [Fact]
    public async Task Handle_ShouldCreateTodoAndReturnId()
    {
        // Arrange
        var command = new CreateTodoCommand(new CreateTodoDto { TodoName = "Test Task" });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeGreaterThan(0);

        var created = await _context.Todos.FindAsync(result);
        created.Should().NotBeNull();
        created!.TodoName.Should().Be("Test Task");
    }
}
