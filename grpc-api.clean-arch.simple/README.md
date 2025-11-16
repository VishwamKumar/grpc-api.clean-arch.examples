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
│       ├── Middleware/               # Global exception middleware
│       ├── Interceptors/             # gRPC exception interceptor
│       ├── Helpers/                  # Utility classes
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

1. **gRPC Request** → `TodoGrpcService` (with `ExceptionInterceptor`)
2. **Map to DTO** → `CreateTodoDto` via AutoMapper
3. **Call Service** → `todoService.CreateAsync(dto)`
4. **Validate** → FluentValidation in service
5. **Repository** → `ITodoRepository.AddAsync()`
6. **Error Handling** → Exceptions caught by interceptor/middleware, logged with Serilog
7. **Response** → Returns result back through the chain (or error response)

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
- **Robust Error Handling** - Global exception middleware and gRPC interceptor
- **Structured Logging** - Serilog with console and file logging
- **Comprehensive Testing** - 27 tests covering all CRUD operations and error scenarios

## 📝 Notes

- Validation is still performed using FluentValidation
- AutoMapper is still used for object mapping
- Clean Architecture layers are preserved
- Domain validation remains in the `Todo` entity
- Uses SQLite database (configured in `appsettings.json`)
- Database files are excluded from source control (see `.gitignore`)

## 🧪 Testing

The project includes comprehensive unit and integration tests covering all CRUD operations:

### Test Coverage
- ✅ **Create** - Unit and integration tests
- ✅ **Read (GetAll)** - Unit and integration tests
- ✅ **Read (GetById)** - Unit and integration tests
- ✅ **Update** - Unit and integration tests
- ✅ **Delete** - Unit and integration tests
- ✅ **Error Scenarios** - Validation errors, not found scenarios

### Test Types
- **Unit Tests**: Test application services with mocked dependencies (`TodoServiceTests.cs`)
- **Integration Tests**: Test repository and database interactions using in-memory database (`TodoServiceIntegrationTests.cs`, `CreateTodoCommandHandlerTests.cs`)

### Test Frameworks
- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Readable assertion library
- **Microsoft.EntityFrameworkCore.InMemory** - In-memory database for integration tests

**Total Tests: 27** (All passing ✅)

## 🛠️ Technology Stack

- **.NET 10.0** - Framework
- **gRPC** - API communication protocol
- **Entity Framework Core 10.0** - ORM
- **SQLite** - Database
- **FluentValidation** - Input validation
- **AutoMapper** - Object mapping
- **Serilog** - Structured logging
- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library

## 📚 Project Structure Details

- **Domain Layer**: Contains entities and domain logic (no dependencies)
- **Application Layer**: Contains services, DTOs, validators, and interfaces
- **Infrastructure Layer**: Contains data access implementation (EF Core, repositories)
- **API Layer**: Contains gRPC services, protocol definitions, middleware, interceptors, and API-specific configurations
- **Tests**: Contains unit and integration tests organized by layer

### Key Components

#### API Layer (`Exp.TodoApp.GrpcApi`)
- `Services/` - gRPC service implementations
- `Middleware/` - Global exception middleware
- `Interceptors/` - gRPC exception interceptor
- `Helpers/` - Utility classes (ServiceHelper, GrpcExceptionHandler)
- `Extensions/` - Service configuration extensions
- `Protos/` - Protocol buffer definitions

#### Application Layer (`Exp.TodoApp.Application`)
- `Services/` - Business logic services (ITodoService, TodoService)
- `Dtos/` - Data transfer objects
- `Validators/` - FluentValidation validators
- `Interfaces/` - Repository interfaces
- `Common/Exceptions/` - Application-specific exceptions

#### Infrastructure Layer (`Exp.TodoApp.Infrastructure`)
- `Persistence/` - EF Core DbContext and repository implementations
- `Extensions/` - Dependency injection configuration
- `Factories/` - DbContext factory for migrations

## 🔧 Configuration

- **Database**: Connection string is configured in `appsettings.json`
- **Swagger**: Can be enabled/disabled via `UseSwagger` setting in `appsettings.json`
- **Logging**: Serilog is configured with console and file logging (see `Serilog` section in `appsettings.json`)
- **Environment-specific**: Settings can be added in `appsettings.Development.json`

## 🛡️ Error Handling & Logging

The project includes comprehensive error handling and structured logging:

### Error Handling
- **Global Exception Middleware**: Handles unhandled exceptions for HTTP requests
- **gRPC Exception Interceptor**: Handles exceptions in gRPC requests and converts them to appropriate gRPC status codes
- **Exception Helpers**: Centralized exception handling logic (`GrpcExceptionHandler`)
- **Validation Errors**: Properly handled with `AppValidationException` returning 400 status
- **Domain Errors**: Handled with `DomainException` returning 400 status

### Structured Logging (Serilog)
- **Console Logging**: Real-time logs in console with structured format
- **File Logging**: Daily rolling log files in `logs/` directory (7-day retention)
- **Log Enrichment**: Includes Environment, MachineName, ThreadId, and LogContext
- **Log Levels**: Configurable via `appsettings.json`

### Log Files
- Logs are written to `logs/log-YYYYMMDD.txt` (one file per day)
- Log files are automatically excluded from source control (see `.gitignore`)

