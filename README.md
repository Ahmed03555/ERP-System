# ERP System — Backend API

A modular Enterprise Resource Planning (ERP) backend built with **.NET**,
applying **Clean Architecture**, **CQRS + MediatR**, and the **Repository +
Unit of Work** patterns.

## 🏗️ Architecture

- **Domain** — Entities, interfaces, and business exceptions (no dependencies)
- **Application** — CQRS Commands/Queries, validation, and cross-cutting
  behaviors (Validation, Logging, Performance, Caching, Exception Handling)
  via MediatR Pipeline Behaviors
- **Infrastructure** — EF Core (Code First), Repository/Unit of Work
  implementation, JWT authentication, Redis caching, audit interceptor
- **WebApi** — Controllers, Swagger, middleware, dynamic permission-based
  authorization

## 🛠️ Tech Stack

- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- MediatR (CQRS) + FluentValidation
- Redis (StackExchange.Redis) — caching with automatic invalidation
- JWT Authentication with Refresh Token rotation
- Dynamic permission-based authorization (Roles ↔ Permissions)
- BCrypt for password hashing
- AutoMapper (selective use for complex mappings)
- xUnit + Moq + FluentAssertions for unit testing

## ✅ Implemented

### Authentication & Authorization
- [x] Register / Login / Refresh Token (with rotation)
- [x] Roles & Permissions management (Create, Assign, List)
- [x] Dynamic permission-based authorization (`[Authorize(Policy = "...")]`)
- [x] Automatic audit trail (`CreatedBy` / `LastModifiedBy`) via EF Core
      `SaveChangesInterceptor`
- [x] Soft-delete enforced globally

### HR Core
- [x] Departments — full CRUD
- [x] Employees — full CRUD
- [x] Attendance — Check-in / Check-out with business rules, history by employee
- [x] Payroll — generation with automatic deduction calculation from
      attendance records, lookup by ID / by employee

### Inventory
- [x] Categories, Products, Warehouses — full CRUD
- [x] `IStockService` — centralized stock increase/decrease with movement
      history (`StockMovements`)
- [x] Stock visibility endpoints (current quantity, movement log)

### Purchasing & Sales
- [x] Suppliers — full CRUD
- [x] Purchase Orders — create, list, view details, receive (with
      **transactional** stock increase)
- [x] Customers — full CRUD
- [x] Sales Orders — create, list, view details, confirm (with
      **transactional** stock decrease and insufficient-stock protection)

### Cross-Cutting
- [x] Global exception handling middleware (`ProblemDetails`-style responses)
- [x] Redis caching on read-heavy queries (List/GetById) with automatic
      cache invalidation on writes
- [x] Unit tests for critical handlers (Moq + FluentAssertions)

### Not yet implemented
- [ ] SignalR real-time notifications
- [ ] Background jobs (Hangfire) for scheduled payroll / low-stock alerts
- [ ] Excel / PDF report exports
- [ ] Docker / docker-compose deployment setup

## 🚀 Getting Started

1. Clone the repo
2. Update the connection string in `appsettings.json` or via User Secrets:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "..." --project ERP.WebApi
   dotnet user-secrets set "JwtSettings:SecretKey" "..." --project ERP.WebApi
   ```
3. Make sure a local Redis instance is running (`localhost:6379` by default)
4. Apply migrations:
   ```bash
   dotnet ef database update -p ERP.Infrastructure -s ERP.WebApi
   ```
5. Run the API:
   ```bash
   dotnet run --project ERP.WebApi
   ```
6. Open Swagger at `https://localhost:{port}/swagger`

## 🧪 Running Tests

```bash
dotnet test
```

## 📌 Notes

This is a personal learning project focused on practicing real-world
architectural patterns — Clean Architecture, CQRS, Repository/UoW,
transactional business workflows, caching, and dynamic authorization —
rather than just consuming a template.
