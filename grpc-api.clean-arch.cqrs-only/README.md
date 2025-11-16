# TodoApp gRPC API - Clean Architecture Template

A production-ready .NET 10 gRPC API template following Clean Architecture principles with CQRS pattern (without MediatR).

## 🏗️ Architecture

This project follows Clean Architecture with the following layers:

- **Domain**: Core business entities and domain logic
- **Application**: Use cases, CQRS commands/queries, validation, custom dispatcher
- **Infrastructure**: Data access (EF Core), external services
- **GrpcApi**: API layer, gRPC services, middleware

### Architecture Diagram

```
┌─────────────────────────────────────────┐
│         GrpcApi (Presentation)         │
│  - gRPC Services                        │
│  - Middleware                           │
│  - Configuration                        │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│      Application (Use Cases)            │
│  - Commands & Queries                   │
│  - Command/Query Handlers               │
│  - DTOs                                 │
│  - Validators (FluentValidation)       │
│  - Custom CQRS Dispatcher               │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│         Domain (Business Logic)          │
│  - Entities                             │
│  - Domain Exceptions                    │
│  - Business Rules                       │
└──────────────┬──────────────────────────┘
               │
┌──────────────▼──────────────────────────┐
│      Infrastructure (Data Access)       │
│  - DbContext                            │
│  - Repositories                         │
│  - EF Core Configurations               │
└─────────────────────────────────────────┘
```

## ✨ Features

- ✅ **Clean Architecture** - Separation of concerns with clear layer boundaries
- ✅ **CQRS Pattern** - Custom implementation without MediatR dependency
- ✅ **gRPC API** - High-performance RPC framework
- ✅ **FluentValidation** - Input validation with clear error messages
- ✅ **Entity Framework Core** - Code-first database approach
- ✅ **Unit & Integration Tests** - Comprehensive test coverage
- ✅ **Global Exception Handling** - Centralized error handling
- ✅ **Health Checks** - Application and database health monitoring
- ✅ **Configuration Validation** - Startup configuration validation
- ✅ **Docker Support** - Containerized deployment ready

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker](https://www.docker.com/) (optional, for containerized deployment)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/) (recommended)

### Running the Application

#### Option 1: Using .NET CLI

```bash
# Restore packages
dotnet restore

# Run the application
dotnet run --project src/Exp.TodoApp.GrpcApi

# Or with watch mode (auto-reload on changes)
dotnet watch run --project src/Exp.TodoApp.GrpcApi
```

#### Option 2: Using Docker

```bash
# Build the Docker image
docker build -t todoapp-grpc-api .

# Run the container
docker run -p 5000:80 -p 5001:443 todoapp-grpc-api
```

#### Option 3: Using Docker Compose

```bash
docker-compose up
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run with code coverage (requires coverlet)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Database Setup

The application uses SQLite by default. The database file is created automatically on first run in `src/Exp.TodoApp.Infrastructure/Databases/`.

#### For Production (SQL Server, PostgreSQL, etc.)

Update the connection string in `appsettings.json` or environment variables:

```json
{
  "ConnectionStrings": {
    "SqliteConnection": "Server=localhost;Database=TodoApp;User Id=sa;Password=YourPassword;"
  }
}
```

#### Database Migrations

```bash
# Navigate to Infrastructure project
cd src/Exp.TodoApp.Infrastructure

# Add a new migration
dotnet ef migrations add InitialCreate --startup-project ../Exp.TodoApp.GrpcApi

# Update database
dotnet ef database update --startup-project ../Exp.TodoApp.GrpcApi
```

## 📁 Project Structure

```
grpc-api.clean-arch.cqrs-only/
├── src/
│   ├── Exp.TodoApp.Domain/              # Domain layer
│   │   ├── Entities/                    # Domain entities
│   │   └── Common/                     # Domain exceptions, base classes
│   │
│   ├── Exp.TodoApp.Application/        # Application layer
│   │   ├── Common/                     # Dispatcher, exceptions
│   │   ├── Extensions/                 # DI registration
│   │   ├── Features/                   # Feature-based organization
│   │   │   └── TodoManager/
│   │   │       ├── Command/           # Commands (Create, Update, Delete)
│   │   │       ├── Queries/           # Queries (GetAll, GetById)
│   │   │       ├── Dtos/              # Data Transfer Objects
│   │   │       └── Validators/        # FluentValidation validators
│   │   ├── Interfaces/
│   │   │   ├── CQRS/                  # CQRS interfaces
│   │   │   └── Persistence/           # Repository interfaces
│   │   └── Profiles/                  # AutoMapper profiles
│   │
│   ├── Exp.TodoApp.Infrastructure/     # Infrastructure layer
│   │   ├── Persistence/               # EF Core DbContext, Repositories
│   │   ├── Extensions/                # DI registration
│   │   └── Factories/                 # DbContext factory for migrations
│   │
│   └── Exp.TodoApp.GrpcApi/           # API layer
│       ├── Services/                  # gRPC service implementations
│       ├── Extensions/                # Middleware, service configuration
│       ├── Middleware/                # Custom middleware
│       ├── Helpers/                   # Helper classes
│       ├── Profiles/                  # AutoMapper profiles
│       └── Protos/                    # gRPC proto files
│
├── tests/
│   └── Exp.TodoApp.Tests/             # Test project
│       ├── UnitTests/                 # Unit tests
│       └── IntegrationTests/         # Integration tests
│
├── Dockerfile                         # Docker configuration
├── docker-compose.yml                 # Docker Compose configuration
└── README.md                          # This file
```

## 🔧 Configuration

Configuration is managed through `appsettings.json` and environment variables.

### Key Settings

- `ConnectionStrings:SqliteConnection` - Database connection string
- `UseSwagger` - Enable/disable Swagger UI (default: true)
- `Logging:LogLevel` - Logging configuration

### Environment Variables

You can override any configuration using environment variables:

```bash
# Windows
set ConnectionStrings__SqliteConnection="Data Source=./todo.db"

# Linux/Mac
export ConnectionStrings__SqliteConnection="Data Source=./todo.db"
```

### User Secrets (Development)

For sensitive data in development:

```bash
dotnet user-secrets init --project src/Exp.TodoApp.GrpcApi
dotnet user-secrets set "ConnectionStrings:SqliteConnection" "your-connection-string" --project src/Exp.TodoApp.GrpcApi
```

## 📚 API Documentation

### Swagger UI

Once running, access Swagger UI at:
- **HTTP**: `http://localhost:5000/swagger`
- **HTTPS**: `https://localhost:5001/swagger`

### gRPC Endpoints

The gRPC service is available at:
- **HTTP/2**: `http://localhost:5000`
- **HTTPS/2**: `https://localhost:5001`

### Health Checks

- **Basic Health**: `http://localhost:5000/health`
- **Readiness Check**: `http://localhost:5000/health/ready`

### gRPC Client Example

```csharp
using Grpc.Net.Client;
using Exp.TodoApp.GrpcApi.Protos;

var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new TodoService.TodoServiceClient(channel);

// Create a todo
var createRequest = new CreateTodoRequest { TodoName = "My Todo" };
var createResponse = await client.CreateAsync(createRequest);

// Get all todos
var getAllResponse = await client.GetAllAsync(new Empty());
```

## 🧪 Testing

### Test Structure

- **Unit Tests**: Test individual components in isolation (handlers, validators)
- **Integration Tests**: Test with real database (using InMemory database)

### Running Specific Tests

```bash
# Run only unit tests
dotnet test --filter Category=Unit

# Run only integration tests
dotnet test --filter Category=Integration

# Run tests for a specific class
dotnet test --filter FullyQualifiedName~CreateTodoCommandHandlerTests
```

### Test Coverage

To generate coverage reports:

```bash
# Install coverlet (if not already installed)
dotnet tool install -g coverlet.console

# Run tests with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```
---

**Built with ❤️ using .NET 10, Clean Architecture, and CQRS**

