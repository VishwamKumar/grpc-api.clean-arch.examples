# 🧩 Exp.TodoApp.GrpcApi

A simple yet complete gRPC-based ToDo application built using **Clean Architecture** principles. This project demonstrates how gRPC can be structured cleanly and made accessible for REST-like interaction using **JSON Transcoding** and **Swagger UI**.

---

# Author

## Vishwa Kumar

- **Email:** vishwa@vishwa.me
- **GitHub:** [Vishwam](https://github.com/vishwamkumar)
- **LinkedIn:** [Vishwa Kumar](https://www.linkedin.com/in/vishwamohan)

Vishwa is the primary architect of the ToDoApp, responsible for the architecture and implementation of these features.

---


## 🏗️ Architecture

This solution is organized using **Clean Architecture**, with each layer in its own project:

- **Domain Layer** – Core business logic and domain entities
- **Application Layer** – Use cases, DTOs, and validation logic
- **Infrastructure Layer** – SQLite persistence using EF Core
- **gRPC API Layer** – Exposes the service via gRPC, with support for REST-like access

---

## 🔧 Technologies Used

- **.NET 9**
- **gRPC** with **gRPC JSON Transcoding**
- **Swagger UI** for testing and exploration
- **Entity Framework Core (EF Core)** with **SQLite**
- **FluentValidation** for request validation
- **AutoMapper** for object mapping
- **xUnit** for testing

---

## 🚀 Features

- Full CRUD support for ToDo items via gRPC
- REST-like testing via JSON Transcoding and Swagger
- Strong separation of concerns using Clean Architecture
- DTO validation using FluentValidation
- Lightweight EF Core data access with SQLite backend

---

## ▶️ Getting Started

1. **Clone the Repository**
2. 
   ```bash
   git clone https://github.com/vishwamkumar/Exp.TodoApp.GrpcApi.git
   cd Exp.TodoApp.GrpcApi

   
2. Run the Application
   
   ```bash
        dotnet run --project src/Exp.TodoApp.GrpcApi
    ```
    ---
3. Test via Swagger (JSON Transcoding)

   Open your browser and navigate to:

   ```bash
     http://localhost:7113/swagger
   ```
   ---

📁 Project Structure

    Exp.TodoApp.GrpcApi/
    ├── src/
    │   ├── Exp.TodoApp.Application/
    │   ├── Exp.TodoApp.Domain/
    │   ├── Exp.TodoApp.Infrastructure/
    │   └── Exp.TodoApp.GrpcApi/        <-- API Host (gRPC + Transcoding + Swagger)
    └── tests/
        └── Exp.TodoApp.Tests/
            ├── IntegrationTests/
            └── UnitTests/        
---

📌 Notes
Even though several gRPC methods could technically reuse request/response DTOs, each method uses its own message types to reinforce separation of concerns and support future scalability and documentation clarity.
