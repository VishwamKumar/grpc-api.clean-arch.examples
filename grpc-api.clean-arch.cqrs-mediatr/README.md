# Todo App - Clean Architecture Template

A reference implementation demonstrating **Clean Architecture**, **CQRS**, and **MediatR** patterns using **gRPC** API with **.NET 10**.

## 🏗️ Architecture Overview

This solution follows Clean Architecture principles with clear separation of concerns:

```
┌─────────────────────────────────────────┐
│         GrpcApi (Presentation)          │
│  - gRPC Services                         │
│  - API Configuration                     │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│      Application (Use Cases)            │
│  - Commands & Queries                    │
│  - Handlers (CQRS)                       │
│  - DTOs & Validators                     │
│  - MediatR Pipeline                     │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│         Domain (Business Logic)          │
│  - Entities                              │
│  - Domain Exceptions                     │
│  - Business Rules                       │
└─────────────────┬───────────────────────┘
                  │
┌─────────────────▼───────────────────────┐
│      Infrastructure (Data Access)        │
│  - EF Core DbContext                    │
│  - Repositories                         │
│  - External Services                    │
└─────────────────────────────────────────┘
```

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Visual Studio 2022/6 / VS Code 
- SQLite (included with .NET)

### Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd grpc-api.clean-arch.cqrs-mediatr
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   cd src/Exp.TodoApp.GrpcApi
   dotnet run
   ```

5. **Access the API**
   - gRPC endpoint: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger` (if enabled)
   - Health Check: `https://localhost:5001/health`

## 📁 Project Structure

```
src/
├── Exp.TodoApp.Domain/              # Domain Layer
│   ├── Entities/                    # Domain entities
│   └── Common/                      # Domain exceptions, base classes
│
├── Exp.TodoApp.Application/        # Application Layer
│   ├── Features/                    # Feature-based organization
│   │   └── TodoManager/
│   │       ├── Command/            # CQRS Commands
│   │       ├── Queries/            # CQRS Queries
│   │       ├── Dtos/               # Data Transfer Objects
│   │       └── Validators/        # FluentValidation validators
│   ├── Common/                      # Shared application logic
│   │   ├── Behaviours/             # MediatR pipeline behaviors
│   │   └── Exceptions/             # Application exceptions
│   ├── Interfaces/                  # Application contracts
│   └── Profiles/                   # AutoMapper profiles
│
├── Exp.TodoApp.Infrastructure/     # Infrastructure Layer
│   ├── Persistence/                 # Data access
│   │   ├── AppDbContext.cs
│   │   └── Repositories/
│   └── Extensions/                  # DI configuration
│
└── Exp.TodoApp.GrpcApi/            # Presentation Layer
    ├── Services/                    # gRPC service implementations
    ├── Extensions/                  # Startup configuration
    └── Protos/                      # gRPC proto files

tests/
└── Exp.TodoApp.Tests/               # Test projects
    ├── UnitTests/                   # Unit tests
    └── IntegrationTests/           # Integration tests
```

## 🎯 Key Patterns & Practices

### CQRS (Command Query Responsibility Segregation)

- **Commands**: Modify state (Create, Update, Delete)
- **Queries**: Read data (GetAll, GetById)
- **Separation**: Different handlers for commands and queries

### MediatR Pipeline

- **ValidationBehavior**: Automatically validates requests using FluentValidation
- **Extensible**: Easy to add logging, caching, or other behaviors

### Clean Architecture Principles

- **Dependency Rule**: Dependencies point inward (Domain has no dependencies)
- **Separation of Concerns**: Each layer has a single responsibility
- **Testability**: Easy to unit test with dependency injection

### Repository Pattern

- **Read Repository**: Optimized for queries (AsNoTracking)
- **Write Repository**: Handles commands and transactions
- **Interface-based**: Easy to mock for testing

## 📝 Adding a New Feature

### Step 1: Create Domain Entity (if needed)
```csharp
// Domain/Entities/NewEntity.cs
public class NewEntity
{
    public int Id { get; private set; }
    public string Name { get; private set; } = default!;
    
    public static NewEntity Create(string name)
    {
        // Validation and creation logic
    }
}
```

### Step 2: Create Application DTOs
```csharp
// Application/Features/NewFeature/Dtos/CreateNewDto.cs
public class CreateNewDto
{
    public string Name { get; set; } = default!;
}
```

### Step 3: Create Command/Query
```csharp
// Application/Features/NewFeature/Command/CreateNew/CreateNewCommand.cs
public record CreateNewCommand(CreateNewDto CreateDto) : IRequest<int>;
```

### Step 4: Create Handler
```csharp
// Application/Features/NewFeature/Command/CreateNew/CreateNewCommandHandler.cs
public class CreateNewCommandHandler(ITodoWriteRepository writeRepo) 
    : IRequestHandler<CreateNewCommand, int>
{
    public async Task<int> Handle(CreateNewCommand request, CancellationToken cancellationToken)
    {
        var entity = NewEntity.Create(request.CreateDto.Name);
        return await writeRepo.AddAsync(entity, cancellationToken);
    }
}
```

### Step 5: Create Validator
```csharp
// Application/Features/NewFeature/Validators/CreateNewCommandValidator.cs
public class CreateNewCommandValidator : AbstractValidator<CreateNewCommand>
{
    public CreateNewCommandValidator()
    {
        RuleFor(x => x.CreateDto.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
}
```

### Step 6: Add Repository Methods (if needed)
```csharp
// Application/Interfaces/Persistence/INewRepository.cs
public interface INewRepository
{
    Task<int> AddAsync(NewEntity entity, CancellationToken cancellationToken = default);
}
```

### Step 7: Implement Repository
```csharp
// Infrastructure/Persistence/NewRepository.cs
public class NewRepository(AppDbContext dbContext) : INewRepository
{
    public async Task<int> AddAsync(NewEntity entity, CancellationToken cancellationToken = default)
    {
        await dbContext.NewEntities.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
```

### Step 8: Register in DI
```csharp
// Infrastructure/Extensions/DependencyInjection.cs
services.AddScoped<INewRepository, NewRepository>();
```

### Step 9: Add gRPC Service Method
```csharp
// GrpcApi/Services/NewGrpcService.cs
public override async Task<CreateNewResponse> CreateNew(CreateNewRequest request, ServerCallContext context)
{
    var dto = mapper.Map<CreateNewDto>(request);
    var result = await mediator.Send(new CreateNewCommand(dto), context.CancellationToken);
    // Handle response
}
```

## 🧪 Running Tests

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true

# Run specific test project
dotnet test tests/Exp.TodoApp.Tests
```

## ⚙️ Configuration

### appsettings.json

```json
{
  "UseSwagger": true,
  "ConnectionStrings": {
    "SqliteConnection": "Data Source=..\\Exp.TodoApp.Infrastructure\\Databases\\ToDo.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Environment Variables

- `ASPNETCORE_ENVIRONMENT`: Set to `Development`, `Staging`, or `Production`
- `ConnectionStrings__SqliteConnection`: Override database connection string

## 🔧 Development Tools

### Recommended Extensions (VS Code)

- C# Dev Kit
- .NET Extension Pack
- gRPC Tools
- REST Client

### Recommended Extensions (Visual Studio)

- ReSharper / Rider
- EF Core Power Tools
- gRPC Tools

## 📚 Key Technologies

- **.NET 10**: Latest .NET framework
- **gRPC**: High-performance RPC framework
- **Entity Framework Core 10**: ORM
- **MediatR**: Mediator pattern implementation
- **FluentValidation**: Validation library
- **AutoMapper**: Object mapping
- **SQLite**: Embedded database

## 🎓 Learning Resources

- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [gRPC Documentation](https://grpc.io/docs/)

## 🤝 Contributing

1. Follow the existing code structure
2. Add tests for new features
3. Update documentation
4. Follow naming conventions
5. Ensure all tests pass

## 🙏 Acknowledgments

- Clean Architecture principles by Robert C. Martin
- CQRS pattern by Greg Young
- MediatR by Jimmy Bogard

---

**Template Version**: 1.0  
**Last Updated**: 2024

