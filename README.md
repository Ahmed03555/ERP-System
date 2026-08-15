# ERP System — Backend API

A modular Enterprise Resource Planning (ERP) backend built with **.NET 8**,
applying **Clean Architecture**, **CQRS + MediatR**, and the **Repository +
Unit of Work** patterns.

## 🏗️ Architecture

- **Domain** — Entities, interfaces, and business exceptions (no dependencies)
- **Application** — CQRS Commands/Queries, validation, and cross-cutting
  behaviors (Validation, Logging, Exception Handling) via MediatR Pipeline
  Behaviors
- **Infrastructure** — EF Core (Code First), Repository/Unit of Work
  implementation, JWT authentication
- **WebApi** — Controllers, Swagger, middleware

## 🛠️ Tech Stack

- ASP.NET Core 8 Web API
- Entity Framework Core 8 (Code First)
- SQL Server
- MediatR (CQRS)
- FluentValidation
- JWT Authentication with Refresh Token rotation
- BCrypt for password hashing

## ✅ Implemented So Far

- [x] Clean Architecture project structure
- [x] MediatR pipeline (Validation, Logging, Performance, Exception Handling behaviors)
- [x] Generic Repository + Unit of Work pattern
- [x] User Registration (with duplicate-email check & password hashing)
- [x] Login + JWT issuance
- [x] Refresh Token flow
- [x] Permission-based Authorization
- [x] HR Module (Employees, Departments, Attendance, Payroll)
- [x] Inventory & Sales Modules
- [x] Customers & Sales Orders (with transactional stock validation)
- [x] Roles & Permissions with dynamic permission-based authorization
## 🚀 Getting Started

1. Clone the repo
2. Update the connection string in `appsettings.json` or via User Secrets
3. Run migrations:
```bash
   dotnet ef database update -p ERP.Infrastructure -s ERP.WebApi
```
4. Run the API:
```bash
   dotnet run --project ERP.WebApi
```
5. Open Swagger at `https://localhost:{port}/swagger`

## 📌 Notes

This is a learning-focused portfolio project — the goal is to practice
real-world architectural patterns (Clean Architecture, CQRS, Repository/UoW)
rather than just consuming a template.
