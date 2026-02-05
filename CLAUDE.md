# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Evvvents2026 is a social activities application built with .NET 10 and React 19. It follows Clean Architecture principles with a CQRS pattern using MediatR for the backend, and a Vite-based React frontend with Material-UI.

## Backend Architecture

### Clean Architecture Layers

The solution is organized into four projects with clear dependency rules:

- **Domain**: Core business entities with no dependencies. Contains entity classes like `Activity`.
- **Persistence**: Data access layer using Entity Framework Core with SQLite. Depends only on Domain.
- **Application**: Business logic layer implementing CQRS with MediatR. Contains Commands and Queries organized by feature. Depends on Domain and Persistence.
- **API**: ASP.NET Core Web API presentation layer. Depends on Application.

### CQRS Pattern with MediatR

All business operations use the CQRS pattern via MediatR:

- **Commands**: Operations that modify state (Create, Edit, Delete). Located in `Application/[Feature]Feature/Commands/`.
- **Queries**: Operations that retrieve data (List, Details). Located in `Application/[Feature]Feature/Queries/`.

Each command/query file contains a nested structure:
```csharp
public class OperationName
{
    public class Command/Query : IRequest<TResponse> { }
    public class Handler : IRequestHandler<Command/Query, TResponse> { }
}
```

### Validation Pipeline

FluentValidation is integrated into the MediatR pipeline via `ValidationBehavior<TRequest, TResponse>` in `Application/Core/ValidationBehavior.cs`. Validators are automatically discovered from the Application assembly and execute before handlers.

### Controllers

All controllers inherit from `BaseApiController` which provides lazy-loaded access to `IMediator`. Controllers are thin and delegate all business logic to MediatR handlers.

## Frontend Architecture

The React frontend is located in the `client/` directory:

- **Stack**: React 19, TypeScript, Vite 7, Material-UI 7
- **Build Tool**: Vite with React Compiler plugin for optimizations
- **Development Server**: Runs on port 3000 with HTTPS support via mkcert
- **Styling**: Material-UI with Emotion for CSS-in-JS

## Common Development Commands

### Backend (.NET)

Build the solution:
```bash
dotnet build
```

Run the API (from API directory):
```bash
cd API
dotnet run
```

Create a new migration:
```bash
dotnet ef migrations add <MigrationName> -p Persistence -s API
```

Apply migrations:
```bash
dotnet ef database update -p Persistence -s API
```

### Frontend (React)

Install dependencies (from client directory):
```bash
cd client
pnpm install
```

Run development server:
```bash
cd client
pnpm dev
```

Build for production:
```bash
cd client
pnpm build
```

Lint code:
```bash
cd client
pnpm lint
```

## Database

- **Provider**: SQLite
- **Location**: `API/events.db`
- **Context**: `AppDbContext` in `Persistence/db/AppDbContext.cs`
- **Seeding**: Handled by `DbInitializer.SeedData()` called in `Program.cs` startup
- **Auto-migration**: Database is automatically migrated on application startup

## Configuration

- **Connection String**: Defined in `API/appsettings.json` under `ConnectionStrings:DefaultConnection`
- **CORS**: Configured to allow `http://localhost:3000` and `https://localhost:3000`
- **MediatR License**: Stored in `appsettings.json` under `MediatR:LicenseKey`

## Key Patterns

### Adding a New Feature

When adding a new feature (e.g., Comments):

1. Create entity in `Domain/entities/`
2. Add DbSet to `AppDbContext` in Persistence
3. Create migration and update database
4. Create Commands in `Application/[Feature]Feature/Commands/`
5. Create Queries in `Application/[Feature]Feature/Queries/`
6. Add validators if needed (FluentValidation)
7. Create controller in `API/Controllers/` inheriting from `BaseApiController`

### Dependency Injection

- MediatR handlers are auto-registered via `AddMediatR()` in `Program.cs`
- DbContext is registered with SQLite connection string
- FluentValidation validators are auto-discovered from Application assembly
- Controllers use constructor injection via primary constructors (C# 12 feature)

## Project Dependencies

The project uses .NET 10 with:
- Entity Framework Core 10.0.1
- MediatR 14.0.0
- FluentValidation (auto-registered)
- SQLite database provider

The client uses modern React:
- React 19.2.x with React Compiler
- Material-UI 7.x
- Axios for HTTP requests
- TypeScript 5.9.x
