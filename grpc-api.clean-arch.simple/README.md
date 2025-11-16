# Simple Clean Architecture - Todo App (gRPC API)

This is a **simplified version** of Clean Architecture **without CQRS and without MediatR**, built with **.NET 10.0** and **gRPC**.

## 🎯 Key Differences from Original

### Removed
- ❌ **MediatR** - No mediator pattern
- ❌ **CQRS** - No separate Command/Query handlers
- ❌ **Pipeline Behaviors** - Validation handled directly in service

### Simplified
- ✅ **Application Service Layer** - Single `ITodoService` interface with all operations
- ✅ **Unified Repository** - Single `ITodoRepository` (combines read/write)
- ✅ **Direct Dependency Injection** - gRPC service directly uses `ITodoService`
- ✅ **Manual Validation** - FluentValidation still used, but called directly in service

## 📁 Architecture

```
grpc-api.clean-arch.simple/
├── src/
│   ├── Exp.TodoApp.Domain/          # Domain entities and business logic
│   ├── Exp.TodoApp.Application/     # Application services, DTOs, validators
│   │   ├── Services/                # ITodoService and TodoService
│   │   ├── Dtos/                    # Data transfer objects
│   │   ├── Validators/              # FluentValidation validators
│   │   └── Interfaces/              # ITodoRepository interface
│   ├── Exp.TodoApp.Infrastructure/ # Data persistence
│   │   ├── Persistence/             # TodoRepository implementation
│   │   └── Extensions/              # Dependency injection setup
│   └── Exp.TodoApp.GrpcApi/         # gRPC API layer
│       ├── Services/                 # gRPC service implementations
│       ├── Protos/                   # Protocol buffer definitions
│       └── Extensions/               # Service configuration
├── tests/
│   └── Exp.TodoApp.Tests/           # Unit and integration tests
│       ├── UnitTests/               # Unit tests
│       └── IntegrationTests/        # Integration tests
├── grpc-api.clean-arch.simple.sln   # Solution file
└── Dockerfile                        # Docker configuration
```

## 🔄 Request Flow

1. **gRPC Request** → `TodoGrpcService`
2. **Map to DTO** → `CreateTodoDto` via AutoMapper
3. **Call Service** → `todoService.CreateAsync(dto)`
4. **Validate** → FluentValidation in service
5. **Repository** → `ITodoRepository.AddAsync()`
6. **Response** → Returns result back through the chain

## 🚀 Getting Started

### Prerequisites
- .NET 10.0 SDK or later
- Docker (optional, for containerized deployment)

### Running the Application

1. **Restore packages**
   ```bash
   dotnet restore grpc-api.clean-arch.simple.sln
   ```

2. **Run the application**
   ```bash
   dotnet run --project src/Exp.TodoApp.GrpcApi
   ```

3. **Test via Swagger**
   - Open browser: `http://localhost:7113/swagger` (if Swagger is enabled)
   - Or use a gRPC client like [Postman](https://www.postman.com/) or [gRPCurl](https://github.com/fullstorydev/grpcurl)

### Running Tests

```bash
# Run all tests
dotnet test

# Run tests for specific project
dotnet test tests/Exp.TodoApp.Tests/Exp.TodoApp.Tests.csproj
```

### Docker Deployment

```bash
# Build Docker image
docker build -t todo-grpc-api .

# Run container
docker run -p 5000:80 todo-grpc-api
```

## 📊 Comparison

| Aspect | Original (CQRS + MediatR) | This Version |
|--------|---------------------------|--------------|
| **Dependencies** | MediatR required | No MediatR |
| **Boilerplate** | High (Command/Query/Handler) | Low (Service only) |
| **Read/Write Separation** | ✅ Yes | ❌ No (unified) |
| **Cross-cutting Concerns** | Pipeline behaviors | Manual validation |
| **Learning Curve** | Steeper | Easier |
| **Testability** | ✅ High | ✅ High |
| **Best For** | Complex domains | Simple CRUD |

## 💡 Benefits

- **Simpler** - Less code to maintain
- **Easier to Understand** - Direct service calls
- **Still Clean** - Maintains Clean Architecture principles
- **No External Dependency** - Removed MediatR
- **Faster Development** - Less boilerplate

## 📝 Notes

- Validation is still performed using FluentValidation
- AutoMapper is still used for object mapping
- Clean Architecture layers are preserved
- Domain validation remains in the `Todo` entity
- Uses SQLite database (configured in `appsettings.json`)
- Database files are excluded from source control (see `.gitignore`)

## 🧪 Testing

The project includes both unit and integration tests:
- **Unit Tests**: Test application services with mocked dependencies
- **Integration Tests**: Test repository and database interactions using in-memory database

Test frameworks used:
- xUnit
- Moq (for mocking)
- FluentAssertions (for readable assertions)
- Microsoft.EntityFrameworkCore.InMemory (for integration tests)

## 🛠️ Technology Stack

- **.NET 10.0** - Framework
- **gRPC** - API communication protocol
- **Entity Framework Core 10.0** - ORM
- **SQLite** - Database
- **FluentValidation** - Input validation
- **AutoMapper** - Object mapping
- **xUnit** - Testing framework

## 📚 Project Structure Details

- **Domain Layer**: Contains entities and domain logic (no dependencies)
- **Application Layer**: Contains services, DTOs, validators, and interfaces
- **Infrastructure Layer**: Contains data access implementation (EF Core, repositories)
- **API Layer**: Contains gRPC services, protocol definitions, and API-specific configurations
- **Tests**: Contains unit and integration tests organized by layer

## 🔧 Configuration

- Database connection string is configured in `appsettings.json`
- Swagger can be enabled/disabled via `UseSwagger` setting in `appsettings.json`
- Environment-specific settings can be added in `appsettings.Development.json`

