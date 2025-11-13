# Simple Clean Architecture - Todo App

This is a **simplified version** of Clean Architecture **without CQRS and without MediatR**.

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
SimpleCleanArch/
├── src/
│   ├── Exp.TodoApp.Domain/          # Domain entities and business logic
│   ├── Exp.TodoApp.Application/     # Application services, DTOs, validators
│   │   ├── Services/                # ITodoService and TodoService
│   │   ├── Dtos/                    # Data transfer objects
│   │   ├── Validators/              # FluentValidation validators
│   │   └── Interfaces/              # ITodoRepository interface
│   ├── Exp.TodoApp.Infrastructure/ # Data persistence
│   │   └── Persistence/             # TodoRepository implementation
│   └── Exp.TodoApp.GrpcApi/         # gRPC API layer
└── Exp.TodoApp.SimpleCleanArch.sln
```

## 🔄 Request Flow

1. **gRPC Request** → `TodoGrpcService`
2. **Map to DTO** → `CreateTodoDto` via AutoMapper
3. **Call Service** → `todoService.CreateAsync(dto)`
4. **Validate** → FluentValidation in service
5. **Repository** → `ITodoRepository.AddAsync()`
6. **Response** → Returns result back through the chain

## 🚀 Getting Started

1. **Restore packages**
   ```bash
   dotnet restore SimpleCleanArch/Exp.TodoApp.SimpleCleanArch.sln
   ```

2. **Run the application**
   ```bash
   dotnet run --project SimpleCleanArch/src/Exp.TodoApp.GrpcApi
   ```

3. **Test via Swagger**
   Open browser: `http://localhost:7113/swagger`

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

