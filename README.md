# 🧩 Clean Architecture Examples - gRPC ToDo API

This repository contains **three complete implementations** of a gRPC-based ToDo application, each demonstrating different approaches to Clean Architecture. All implementations follow Clean Architecture principles but vary in complexity and patterns used.

---

## 📚 Repository Overview

This repository provides **three distinct examples** of Clean Architecture implementation:

1. **CQRS + MediatR** - Full-featured implementation with MediatR mediator pattern
2. **CQRS Only** - CQRS pattern with custom dispatcher (no MediatR dependency)
3. **Simple** - Traditional service layer approach (no CQRS, no MediatR)

Each example is a complete, working application that demonstrates how to structure a gRPC API using Clean Architecture principles.

---

## 🏗️ Common Architecture

All three implementations share the same **Clean Architecture** layer structure:

- **Domain Layer** – Core business logic and domain entities
- **Application Layer** – Use cases, DTOs, and validation logic (varies by implementation)
- **Infrastructure Layer** – SQLite persistence using EF Core
- **gRPC API Layer** – Exposes the service via gRPC, with support for REST-like access via JSON Transcoding

---

## 🔧 Common Technologies

All implementations use:

- **.NET 10**
- **gRPC** with **gRPC JSON Transcoding**
- **Swagger UI** for testing and exploration
- **Entity Framework Core (EF Core)** with **SQLite**
- **FluentValidation** for request validation
- **AutoMapper** for object mapping
- **xUnit** for testing

---

## 📦 Implementation Details

### 1️⃣ CQRS + MediatR Implementation

**Location:** `grpc-api.clean-arch.cqrs-mediatr/`

#### Architecture Pattern
- **CQRS** (Command Query Responsibility Segregation) with separate Command/Query handlers
- **MediatR** mediator pattern for request handling
- **Pipeline Behaviors** for cross-cutting concerns (validation, logging, etc.)
- **Separate Read/Write Repositories** (CQRS pattern)

#### Key Characteristics
- Uses `MediatR` library for dispatching commands and queries
- Validation handled via `IPipelineBehavior<TRequest, TResponse>`
- Each operation has dedicated Command/Query classes and Handlers
- Feature-based folder structure (Commands and Queries organized by feature)

#### ✅ Pros
- **Industry Standard**: MediatR is widely used and well-documented
- **Pipeline Behaviors**: Easy to add cross-cutting concerns (logging, caching, validation)
- **Decoupled**: Handlers are completely decoupled from the API layer
- **Testable**: Easy to unit test handlers in isolation
- **Scalable**: Excellent for large applications with many features
- **Separation of Concerns**: Clear separation between commands (writes) and queries (reads)
- **Extensible**: Easy to add new pipeline behaviors (authorization, performance monitoring, etc.)

#### ❌ Cons
- **External Dependency**: Requires MediatR NuGet package
- **Boilerplate**: More code files (Command, CommandHandler, Validator for each operation)
- **Learning Curve**: Requires understanding of MediatR patterns
- **Overhead**: Slight performance overhead due to mediator pattern
- **Complexity**: More moving parts for simple CRUD operations

#### 🎯 When to Choose This
- **Large applications** with complex business logic
- **Teams familiar** with MediatR and CQRS patterns
- **Need for cross-cutting concerns** (logging, caching, authorization, etc.)
- **Multiple developers** working on different features
- **Future scalability** is a priority
- **Enterprise applications** requiring strict separation of concerns

---

### 2️⃣ CQRS Only Implementation

**Location:** `grpc-api.clean-arch.cqrs-only/`

#### Architecture Pattern
- **CQRS** (Command Query Responsibility Segregation) with separate Command/Query handlers
- **Custom Dispatcher** implementation (no MediatR dependency)
- **Separate Read/Write Repositories** (CQRS pattern)
- Validation handled within the custom Dispatcher

#### Key Characteristics
- Custom `IDispatcher` interface and `Dispatcher` implementation
- Manual handler registration via reflection
- Validation integrated into dispatcher logic
- Same CQRS structure as MediatR version, but without external dependency

#### ✅ Pros
- **No External Dependency**: No MediatR package required
- **CQRS Benefits**: Still maintains command/query separation
- **Full Control**: Complete control over dispatcher behavior
- **Lightweight**: No third-party library overhead
- **Customizable**: Can customize dispatcher to specific needs
- **Separation of Concerns**: Maintains clear separation between reads and writes
- **Testable**: Handlers remain testable in isolation

#### ❌ Cons
- **Custom Code**: Need to maintain custom dispatcher implementation
- **Manual Registration**: Handler registration requires reflection code
- **Limited Pipeline**: No built-in pipeline behavior support (must implement manually)
- **More Code**: Need to implement validation and cross-cutting concerns manually
- **Less Standardized**: Not following industry-standard patterns
- **Maintenance**: Custom dispatcher needs to be maintained and tested

#### 🎯 When to Choose This
- **Want CQRS benefits** without external dependencies
- **Prefer full control** over request handling
- **Medium-sized applications** with moderate complexity
- **Teams comfortable** with custom implementations
- **Budget constraints** (avoiding external package dependencies)
- **Specific requirements** that MediatR doesn't meet

---

### 3️⃣ Simple Implementation

**Location:** `grpc-api.clean-arch.simple/`

#### Architecture Pattern
- **Traditional Service Layer** approach
- **Unified Repository** pattern (single repository for read/write)
- **Direct Dependency Injection** (no mediator pattern)
- Manual validation in service methods

#### Key Characteristics
- `ITodoService` interface with all CRUD operations
- `ITodoRepository` unified repository (no read/write separation)
- Validation called directly in service methods
- Direct service injection into gRPC service

#### ✅ Pros
- **Simplest Approach**: Easiest to understand and implement
- **Less Boilerplate**: Fewer files and classes
- **Faster Development**: Quick to build and modify
- **No External Dependencies**: No MediatR or custom dispatcher
- **Straightforward**: Direct flow from API → Service → Repository
- **Easy Onboarding**: New developers can understand quickly
- **Low Overhead**: Minimal abstraction layers

#### ❌ Cons
- **No CQRS**: Read and write operations not separated
- **Limited Scalability**: Can become complex as application grows
- **Manual Cross-Cutting**: Must manually add logging, caching, etc.
- **Tight Coupling**: Service layer directly coupled to API layer
- **Less Flexible**: Harder to add cross-cutting concerns later
- **No Pipeline**: No automatic validation or behavior pipeline

#### 🎯 When to Choose This
- **Small to medium applications** with simple CRUD operations
- **Rapid prototyping** and proof-of-concept projects
- **Learning Clean Architecture** (start here to understand basics)
- **Small teams** (1-2 developers)
- **Simple business logic** without complex workflows
- **Time-constrained projects** requiring quick delivery
- **Microservices** with limited scope

---

## 📊 Comparison Matrix

| Aspect | CQRS + MediatR | CQRS Only | Simple |
|--------|----------------|-----------|--------|
| **Complexity** | High | Medium | Low |
| **Boilerplate** | High | Medium | Low |
| **External Dependencies** | MediatR | None | None |
| **CQRS Pattern** | ✅ Yes | ✅ Yes | ❌ No |
| **Pipeline Behaviors** | ✅ Built-in | ❌ Manual | ❌ Manual |
| **Read/Write Separation** | ✅ Yes | ✅ Yes | ❌ No |
| **Learning Curve** | Steep | Moderate | Easy |
| **Scalability** | Excellent | Good | Limited |
| **Testability** | Excellent | Excellent | Good |
| **Development Speed** | Slower | Moderate | Faster |
| **Best For** | Enterprise/Large Apps | Medium Apps | Small/Simple Apps |

---

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK
- Git

### Clone the Repository

```bash
git clone https://github.com/vishwamkumar/apis.clean-arch.examples.git
cd apis.clean-arch.examples
```

### Run an Implementation

Choose one of the three implementations:

#### Option 1: CQRS + MediatR
```bash
cd grpc-api.clean-arch.cqrs-mediatr
dotnet restore
dotnet run --project src/Exp.TodoApp.GrpcApi
```

#### Option 2: CQRS Only
```bash
cd grpc-api.clean-arch.cqrs-only
dotnet restore
dotnet run --project src/Exp.TodoApp.GrpcApi
```

#### Option 3: Simple
```bash
cd grpc-api.clean-arch.simple
dotnet restore
dotnet run --project src/Exp.TodoApp.GrpcApi
```

### Test via Swagger

Once the application is running, open your browser and navigate to:

```
http://localhost:7113/swagger
```

You can test all CRUD operations via the Swagger UI using JSON Transcoding.

---

## 📁 Project Structure

Each implementation follows the same Clean Architecture structure:

```
ImplementationName/
├── src/
│   ├── Exp.TodoApp.Domain/          # Domain entities and business logic
│   ├── Exp.TodoApp.Application/     # Application layer (varies by implementation)
│   ├── Exp.TodoApp.Infrastructure/  # Data persistence (EF Core + SQLite)
│   └── Exp.TodoApp.GrpcApi/         # gRPC API layer (gRPC + Transcoding + Swagger)
└── tests/
    └── Exp.TodoApp.Tests/
        ├── IntegrationTests/
        └── UnitTests/
```

### Application Layer Differences

**CQRS + MediatR:**
```
Application/
├── Features/
│   └── TodoManager/
│       ├── Command/          # Commands and Handlers
│       ├── Query/            # Queries and Handlers
│       ├── Dtos/             # Data Transfer Objects
│       └── Validators/       # FluentValidation validators
├── Common/
│   └── Behaviours/           # Pipeline behaviors (validation, etc.)
└── Extensions/
```

**CQRS Only:**
```
Application/
├── Features/
│   └── TodoManager/
│       ├── Command/          # Commands and Handlers
│       ├── Query/            # Queries and Handlers
│       ├── Dtos/             # Data Transfer Objects
│       └── Validators/       # FluentValidation validators
├── Common/
│   └── Dispatcher.cs         # Custom dispatcher implementation
└── Extensions/
```

**Simple:**
```
Application/
├── Services/                 # ITodoService and TodoService
├── Dtos/                     # Data Transfer Objects
├── Validators/               # FluentValidation validators
└── Interfaces/
    └── Persistence/          # ITodoRepository interface
```

---

## 🎓 Learning Path Recommendation

1. **Start with Simple** - Understand Clean Architecture basics
2. **Move to CQRS Only** - Learn CQRS pattern without external dependencies
3. **Advance to CQRS + MediatR** - Master enterprise patterns with MediatR

---

## 💡 Decision Guide

### Choose **Simple** if:
- ✅ Building a small application or prototype
- ✅ Team is new to Clean Architecture
- ✅ Need to deliver quickly
- ✅ Application has straightforward CRUD operations

### Choose **CQRS Only** if:
- ✅ Want CQRS benefits without external dependencies
- ✅ Prefer full control over implementation
- ✅ Building medium-sized applications
- ✅ Team is comfortable with custom code

### Choose **CQRS + MediatR** if:
- ✅ Building enterprise or large-scale applications
- ✅ Need pipeline behaviors for cross-cutting concerns
- ✅ Team is experienced with MediatR
- ✅ Application will grow significantly
- ✅ Multiple developers working on different features

---

## 📌 Notes

- Even though several gRPC methods could technically reuse request/response DTOs, each method uses its own message types to reinforce separation of concerns and support future scalability and documentation clarity.
- All implementations maintain the same domain model and business logic, demonstrating that Clean Architecture principles can be applied with different patterns.
- The Infrastructure layer is identical across all implementations, showing that persistence concerns are independent of application layer patterns.

---

## 👤 Author

### Vishwa Kumar

- **Email:** vishwa@vishwa.me
- **GitHub:** [Vishwam](https://github.com/vishwamkumar)
- **LinkedIn:** [Vishwa Kumar](https://www.linkedin.com/in/vishwamohan)

Vishwa is the primary architect of these implementations, responsible for the architecture and implementation of all three approaches.

---

## 📄 License

See [LICENSE](LICENSE) file for details.
